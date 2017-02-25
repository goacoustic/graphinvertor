using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Asuka.InvertTree
{
    public class GraphInvertorTests
    {
        [Fact]
        public void Invert_ShouldReturnInvertedGraph()
        {
            // arrange
            var target = new GraphInvertor();
            var root = new Node
            {
                Value = "1",
                Childs = new List<Node>
                {
                    new Node {Value = "2", Childs = new List<Node> {new Node("3"), new Node("4")}},
                    new Node {Value = "5", Childs = new List<Node> {new Node("6"), new Node("7")}}
                }
            };

            // act
            var result = target.Invert(root);

            // assert
            var oldRoot = result.Single(n => n.Value == "1");
            oldRoot.Childs.Should().BeEmpty();

            var secondNode = result.Single(n => n.Value == "2");
            secondNode.Childs.Should().HaveCount(1).And.Contain(oldRoot);
            var fifthNode = result.Single(n => n.Value == "5");
            fifthNode.Childs.Should().HaveCount(1).And.Contain(oldRoot);

            // old leafs
            var thirdNode = result.Single(n => n.Value == "3");
            thirdNode.Childs.Should().HaveCount(1).And.Contain(secondNode);
            var forthNode = result.Single(n => n.Value == "4");
            forthNode.Childs.Should().HaveCount(1).And.Contain(secondNode);

            var sixthNode = result.Single(n => n.Value == "6");
            sixthNode.Childs.Should().HaveCount(1).And.Contain(fifthNode);
            var seventhNode = result.Single(n => n.Value == "7");
            seventhNode.Childs.Should().HaveCount(1).And.Contain(fifthNode);
        }

        [Fact]
        public void Invert_SimpleTree_ShouldReturnInvertedGraph()
        {
            // arrange
            var target = new GraphInvertor();
            var root = new Node
            {
                Value = "1",
                Childs = new List<Node>
                {
                    new Node("2")
                }
            };

            // act
            var result = target.Invert(root);

            // assert
            var oldRoot = result.Single(n => n.Value == "1");
            oldRoot.Childs.Should().BeEmpty();

            var secondNode = result.Single(n => n.Value == "2");
            secondNode.Childs.Should().HaveCount(1).And.Contain(oldRoot);
        }

        [Fact]
        public void Invert_ComplicatedTree_ShouldReturnInvertedGraph()
        {
            // arrange
            var target = new GraphInvertor();
            var root = new Node
            {
                Value = "1",
                Childs = new List<Node>
                {
                    new Node {Value = "2", Childs = new List<Node> {new Node("3"), new Node("4")}},
                    new Node {Value = "5", Childs = new List<Node> {new Node("6"), new Node {Value = "7", Childs = new List<Node>
                    {
                        new Node("9"), new Node("10"), new Node("11")
                    }}}},
                    new Node("8")
                }
            };

            // act
            var result = target.Invert(root);

            // assert
            var oldRoot = result.Single(n => n.Value == "1");
            oldRoot.Childs.Should().BeEmpty();

            var secondNode = result.Single(n => n.Value == "2");
            secondNode.Childs.Should().HaveCount(1).And.Contain(oldRoot);
            var fifthNode = result.Single(n => n.Value == "5");
            fifthNode.Childs.Should().HaveCount(1).And.Contain(oldRoot);

            // old leafs
            var thirdNode = result.Single(n => n.Value == "3");
            thirdNode.Childs.Should().HaveCount(1).And.Contain(secondNode);
            var forthNode = result.Single(n => n.Value == "4");
            forthNode.Childs.Should().HaveCount(1).And.Contain(secondNode);

            var sixthNode = result.Single(n => n.Value == "6");
            sixthNode.Childs.Should().HaveCount(1).And.Contain(fifthNode);
            var seventhNode = result.Single(n => n.Value == "7");
            seventhNode.Childs.Should().HaveCount(1).And.Contain(fifthNode);

            var eightsNode = result.Single(n => n.Value == "8");
            eightsNode.Childs.Should().HaveCount(1).And.Contain(oldRoot);

            result.Single(n => n.Value == "9").Childs.Should().HaveCount(1).And.Contain(seventhNode);
            result.Single(n => n.Value == "10").Childs.Should().HaveCount(1).And.Contain(seventhNode);
            result.Single(n => n.Value == "11").Childs.Should().HaveCount(1).And.Contain(seventhNode);
        }
    }
}