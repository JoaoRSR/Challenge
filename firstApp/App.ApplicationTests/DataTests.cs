using App.Application.Data.Command;
using App.Application.Data.Queries;
using App.Application.Interfaces;
using App.Domain.Data.Requests;
using App.Domain.Data.Responses;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace App.ApplicationTests;

[TestClass]
public class DataTests
{
    private Mock<IDataService> _mockDataService;
    private Mock<IRepositoryService> _mockRepositoryService;

    [TestInitialize]
    public void Setup()
    {
        _mockDataService = new Mock<IDataService>();
        _mockRepositoryService = new Mock<IRepositoryService>();
    }

    #region CreateDbTableWithUserData
    [TestMethod]
    [DataRow(0, "", 1)]
    [DataRow(3, "", 2)]
    [DataRow(0, "history", 3)]
    public async Task CreateDbTableWithUserDataTest_containingPostWithTagAndMinimumOfPostReactions_Works(
        int minimumOfPostReactions,
        string tag,
        int expectedResultValue)
    {
        CancellationToken cancellationToken = new();

        _mockDataService.Setup(i => i.GetAllPostsTagsAndReactionsAsync(cancellationToken))
                     .ReturnsAsync(new List<Posts>()
                     {
                         new Posts {
                             Id = 1,
                             Reactions = 2,
                             Tags = new string[] {"history", "fiction"},
                             UserId = 1
                         },
                         new Posts {
                             Id = 4,
                             Reactions = 4,
                             Tags = new string[] {"fiction"},
                             UserId = 1
                         },
                         new Posts {
                             Id = 2,
                             Reactions = 1,
                             Tags = new string[] {"french", "fiction"},
                             UserId = 2
                         },
                         new Posts {
                             Id = 3,
                             Reactions = 3,
                             Tags = new string[] {},
                             UserId = 3
                         }
                     });

        _mockDataService.Setup(i => i.GetAllUserIDsFromTodosAsync(cancellationToken))
                     .ReturnsAsync(new List<int>()
                     {1, 2, 2, 2, 3, 3, 3, 3});

        List<UsersActivityCount> data = MockData.GetMockUsersActivityCountResponseResult(ExpectedResultValue: expectedResultValue);

        CreateDbTableWithUserDataCommand command = new(containingPostTag: tag, MinimumOfPostReactions: minimumOfPostReactions);

        await new CreateDbTableWithUserDataHandler(_mockRepositoryService.Object, _mockDataService.Object)
            .Handle(
                command, cancellationToken
            );

        _mockRepositoryService.Verify(
            m => m.AddOrCreateUsersActivityCountToDatabaseAsync(It.Is<List<UsersActivityCount>>(
                    s => ArrayCompare(s, data)
                ), It.IsAny<CancellationToken>()),
            Times.Once);
    }
    #endregion

    #region CreateDbTableWithPostData
    [TestMethod]
    public async Task CreateDbTableWithPostDataTest_ValidParameters_Works()
    {
        CancellationToken cancellationToken = new();

        _mockDataService.Setup(i => i.GetAllPostsTagsAndReactionsAsync(cancellationToken))
                     .ReturnsAsync(new List<Posts>()
                     {
                         new Posts {
                             Id = 1,
                             Reactions = 2,
                             Tags = new string[] {"history", "fiction"},
                             UserId = 1
                         },
                         new Posts {
                             Id = 1,
                             Reactions = 1,
                             Tags = new string[] {"french", "fiction"},
                             UserId = 2
                         },
                         new Posts {
                             Id = 1,
                             Reactions = 3,
                             Tags = new string[] {},
                             UserId = 3
                         }
                     });

        _mockDataService.Setup(i => i.GetUserNameFromUsersAsync(cancellationToken))
                     .ReturnsAsync(new List<Users>()
                     {
                         new Users {
                             Id = 1,
                             UserName = "Name1",
                         },
                         new Users {
                             Id = 2,
                             UserName = "Name2",
                         },
                         new Users {
                             Id = 3,
                             UserName = "Name3",
                         }
                     });

        var data = new List<PostsToSave>()
        {
            new PostsToSave()
            {
                Id = 1,
                HasFictionTag = true,
                HasFrenchTag = false,
                HasMoreThanTwoReactions = false,
                UserId = 1,
                UserName= "Name1",
            },
            new PostsToSave()
            {
                Id = 1,
                HasFictionTag = true,
                HasFrenchTag = true,
                HasMoreThanTwoReactions = false,
                UserId = 2,
                UserName= "Name2",
            },
            new PostsToSave()
            {
                Id = 1,
                HasFictionTag = false,
                HasFrenchTag = false,
                HasMoreThanTwoReactions = true,
                UserId = 3,
                UserName= "Name3",
            }
        };

        CreateDbTableWithPostDataCommand command = new();

        await new CreateDbTableWithPostDataHandler(_mockRepositoryService.Object, _mockDataService.Object)
            .Handle(
                command, cancellationToken
            );

        _mockRepositoryService.Verify(
            m => m.AddOrCreatePostsToDatabaseAsync(It.Is<List<PostsToSave>>(
                    s => s.Count == 3
                    && Compare(s[0], data[0])
                    && Compare(s[1], data[1])
                    && Compare(s[2], data[2])
                ), It.IsAny<CancellationToken>()),
            Times.Once);
    }
    #endregion

    #region GetPostsFromUsers
    [TestMethod]
    public async Task GetPostsFromUsersTest_ValidParameters_Works()
    {
        CancellationToken cancellationToken = new();

        _mockDataService.Setup(i => i.GetAllUsersWithCardTypeAsync(It.IsAny<string>(), cancellationToken))
                     .ReturnsAsync(new List<Users>()
                     {
                         new Users {
                             Id = 1,
                         },
                         new Users {
                             Id = 1,
                         },
                         new Users {
                             Id = 2,
                         },
                         new Users {
                             Id = 3,
                         },
                         new Users {
                             Id = 3,
                         },
                         new Users {
                             Id = 3,
                         },
                     });

        var expectedResult = new List<Posts>();

        for (int n = 1; n < 4; n++)
        {
            _mockDataService
            .Setup(i => i.GetAllPostsAsync(n, cancellationToken))
            .ReturnsAsync(new List<Posts>() {
                new Posts
                {
                    Id = n,
                    Reactions = 2,
                    Tags = new string[] { "history", "fiction" },
                    UserId = n,
                    Body = $"body{n}",
                    Title = $"title{n}"
                }
            });

            expectedResult.Add(new Posts
            {
                Id = n,
                Reactions = 2,
                Tags = new string[] { "history", "fiction" },
                UserId = n,
                Body = $"body{n}",
                Title = $"title{n}"
            });
        }

        GetPostsFromUsersQuery query = new("cardType");

        var result = await new GetPostsFromUsersHandler(_mockDataService.Object)
            .Handle(
                query, cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }
    #endregion

    #region GetTodosFromUsers
    [TestMethod]
    [DataRow(0, 1)]
    [DataRow(2, 2)]
    [DataRow(4, 3)]
    public async Task GetTodosFromUsersTest_ValidParametersWithMinNumberOfPost_Works(
        int MinOfPosts,
        int expectedResultValue)
    {
        CancellationToken cancellationToken = new();

        _mockDataService.Setup(i => i.GetAllUserIdFromPostsAsync(cancellationToken))
                     .ReturnsAsync(new List<int>() { 1, 1, 1, 1, 1, 1, 2, 2, 2, 3 });

        var expectedResult = new List<Todos>();

        for (int n = 1; n < 4; n++)
        {
            _mockDataService
            .Setup(i => i.GetAllTodosAsync(n, cancellationToken))
            .ReturnsAsync(new List<Todos>() {
                new Todos
                {
                    Id = n,
                    UserId = n,
                    Completed = false,
                    Todo = $"todo{n}"
                }
            });
        }

        expectedResult = MockData.GetMockTodosResponseResult(expectedResultValue: expectedResultValue);

        GetTodosFromUsersQuery query = new(MinNumberOfPost: MinOfPosts);

        var result = await new GetTodosFromUsersHandler(_mockDataService.Object)
            .Handle(
                query, cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }
    #endregion

    private static bool ArrayCompare<T>(IEnumerable<T> List1, IEnumerable<T> List2)
    {
        if (List1.Count() != List2.Count())
            return false;

        for (int i = 0; i < List1.Count(); i++)
        {
            if (!Compare(List1.ElementAt(i), List2.ElementAt(i)))
                return false;
        }

        return true;
    }

    private static bool Compare<T>(T Object1, T object2)
    {
        //Get the type of the object
        Type type = typeof(T);

        //return false if any of the object is false
        if (Object1 == null || object2 == null)
            return false;

        //Loop through each properties inside class and get values for the property from both the objects and compare
        foreach (System.Reflection.PropertyInfo property in type.GetProperties())
        {
            if (property.Name != "ExtensionData")
            {
                string Object1Value = string.Empty;
                string Object2Value = string.Empty;
                if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                    Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                    Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                if (Object1Value.Trim() != Object2Value.Trim())
                {
                    return false;
                }
            }
        }
        return true;
    }
}