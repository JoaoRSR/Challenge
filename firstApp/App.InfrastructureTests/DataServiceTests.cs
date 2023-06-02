using App.Domain.Data.Responses;
using App.Infrastructure.Dummyjson.Client;
using App.Infrastructure.Dummyjson.Models.Responses;
using App.Infrastructure.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace App.InfrastructureTests;

[TestClass]
public class DataServiceTests
{
    private Mock<IDummyjsonClient> _mockDummyjsonClient;

    [TestInitialize]
    public void Setup() => _mockDummyjsonClient = new Mock<IDummyjsonClient>();

    [TestMethod]
    public async Task GetAllPostsTagsAndReactionsTest_ReturnsCorrectData()
    {
        CancellationToken cancellationToken = new();

        _mockDummyjsonClient.Setup(i => i.GetAllPostsTagsAndReactionsAsync(cancellationToken))
                     .ReturnsAsync(new List<PostData>()
                     {
                         new PostData {
                             Id = 1,
                             Reactions = 2,
                             Tags = new string[] {"history", "fiction"},
                             UserId = 1,
                             Body = "Body1",
                             Title = "Tilte1",
                         },
                         new PostData {
                             Id = 4,
                             Reactions = 4,
                             Tags = new string[] {"fiction"},
                             UserId = 1,
                             Body = "Body2",
                             Title = "Tilte2",
                         }
                     });

        List<Posts> expectedResult = new List<Posts>()
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
                         }
                     };

        var result = await new DataService(_mockDummyjsonClient.Object)
            .GetAllPostsTagsAndReactionsAsync(cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetAllUserIDFromPostsTest_ReturnsCorrectData()
    {
        CancellationToken cancellationToken = new();

        _mockDummyjsonClient.Setup(i => i.GetAllUserIdFromPostsAsync(cancellationToken))
                     .ReturnsAsync(new List<PostData>()
                     {
                         new PostData {
                             Id = 1,
                             Reactions = 2,
                             Tags = new string[] {"history", "fiction"},
                             UserId = 1,
                             Body = "Body1",
                             Title = "Tilte1",
                         },
                         new PostData {
                             Id = 4,
                             Reactions = 4,
                             Tags = new string[] {"fiction"},
                             UserId = 1,
                             Body = "Body2",
                             Title = "Tilte2",
                         },
                         new PostData {
                             Id = 2,
                             Reactions = 4,
                             Tags = new string[] {"fiction"},
                             UserId = 2,
                             Body = "Body2",
                             Title = "Tilte2",
                         }
                     });

        List<int> expectedResult = new List<int>(){1, 1, 2};

        var result = await new DataService(_mockDummyjsonClient.Object)
            .GetAllUserIdFromPostsAsync(cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetAllPostsTest_ReturnsCorrectData()
    {
        CancellationToken cancellationToken = new();

        _mockDummyjsonClient.Setup(i => i.GetAllPostsAsync(1, cancellationToken))
                  .ReturnsAsync(new List<PostData>()
                     {
                         new PostData {
                             Id = 1,
                             Reactions = 2,
                             Tags = new string[] {"history", "fiction"},
                             UserId = 1,
                             Body = "Body1",
                             Title = "Tilte1",
                         },
                         new PostData {
                             Id = 4,
                             Reactions = 4,
                             Tags = new string[] {"fiction"},
                             UserId = 1,
                             Body = "Body2",
                             Title = "Tilte2",
                         }
                     });

        List<Posts> expectedResult = new List<Posts>()
                     {
                         new Posts {
                             Id = 1,
                             Reactions = 2,
                             Tags = new string[] {"history", "fiction"},
                             UserId = 1,
                             Body = "Body1",
                             Title = "Tilte1",
                         },
                         new Posts {
                             Id = 4,
                             Reactions = 4,
                             Tags = new string[] {"fiction"},
                             UserId = 1,
                             Body = "Body2",
                             Title = "Tilte2",
                         }
                     };

        var result = await new DataService(_mockDummyjsonClient.Object)
            .GetAllPostsAsync(1, cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetAllTodosTest_ReturnsCorrectData()
    {
        CancellationToken cancellationToken = new();

        _mockDummyjsonClient.Setup(i => i.GetAllTodosFromTodosAsync(1, cancellationToken))
                  .ReturnsAsync(new List<TodoData>()
                     {
                         new TodoData {
                             Id = 1,
                             UserId = 1,
                             Completed = true,
                             Todo = "todo1"
                         },
                         new TodoData {
                             Id = 2,
                             UserId = 1,
                             Completed = false,
                             Todo = "todo2"
                         }
                     });

        List<Todos> expectedResult = new List<Todos>()
                     {
                         new Todos {
                             Id = 1,
                             UserId = 1,
                             Completed = true,
                             Todo = "todo1"
                         },
                         new Todos {
                             Id = 2,
                             UserId = 1,
                             Completed = false,
                             Todo = "todo2"
                         }
                     };

        var result = await new DataService(_mockDummyjsonClient.Object)
            .GetAllTodosAsync(1, cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetAllUserIDsFromTodosTest_ReturnsCorrectData()
    {
        CancellationToken cancellationToken = new();

        _mockDummyjsonClient.Setup(i => i.GetAllTodosAsync(cancellationToken))
                  .ReturnsAsync(new List<TodoData>()
                     {
                         new TodoData {
                             Id = 1,
                             UserId = 1,
                             Completed = true,
                             Todo = "todo1"
                         },
                         new TodoData {
                             Id = 2,
                             UserId = 1,
                             Completed = false,
                             Todo = "todo2"
                         },
                         new TodoData {
                             Id = 3,
                             UserId = 2,
                             Completed = false,
                             Todo = "todo3"
                         }
                     });

        List<int> expectedResult = new List<int>(){1, 1, 2};

        var result = await new DataService(_mockDummyjsonClient.Object)
            .GetAllUserIDsFromTodosAsync(cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetAllUsersWithCardTypeTest_ReturnsCorrectData()
    {
        CancellationToken cancellationToken = new();

        _mockDummyjsonClient.Setup(i => i.GetAllUsersIDByCardTypeInformationFromUsersAsync("cardType", cancellationToken))
                  .ReturnsAsync(new List<UserData>()
                     {
                         new UserData {
                             Id = 1,
                             UserName = "name1",
                         },
                         new UserData {
                             Id = 2,
                             UserName = "name2",
                         },
                     });

        List<Users> expectedResult = new List<Users>() 
        {
            new Users()
            {
                Id = 1,
            },
            new Users()
            {
                Id = 2,
            }
        };

        var result = await new DataService(_mockDummyjsonClient.Object)
            .GetAllUsersWithCardTypeAsync("cardType", cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task GetUserNameFromUsersTest_ReturnsCorrectData()
    {
        CancellationToken cancellationToken = new();

        _mockDummyjsonClient.Setup(i => i.GetUserNameFromUsersAsync(cancellationToken))
                  .ReturnsAsync(new List<UserData>()
                     {
                         new UserData {
                             Id = 1,
                             UserName = "name1",
                         },
                         new UserData {
                             Id = 2,
                             UserName = "name2",
                         },
                     });

        List<Users> expectedResult = new List<Users>()
        {
            new Users()
            {
                Id = 1,
                UserName = "name1",
            },
            new Users()
            {
                Id = 2,
                UserName = "name2",
            }
        };

        var result = await new DataService(_mockDummyjsonClient.Object)
            .GetUserNameFromUsersAsync(cancellationToken
            );

        result.Should().NotBeNull();

        //verify expected behavior
        result.Should().BeEquivalentTo(expectedResult);
    }
}