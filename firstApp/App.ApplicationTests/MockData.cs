using App.Domain.Data.Requests;
using App.Domain.Data.Responses;

namespace App.ApplicationTests;
internal static class MockData
{
    internal static List<Todos> GetMockTodosResponseResult(int expectedResultValue)
    {
        var response = new List<Todos>();

        if (expectedResultValue == 1)
        {
            response.populateListTodos(1);
            response.populateListTodos(2);
            response.populateListTodos(3);
        }

        if (expectedResultValue == 2)
        {
            response.populateListTodos(1);
            response.populateListTodos(2);
        }

        if (expectedResultValue == 3)
        {
            response.populateListTodos(1);
        }

        return response;
    }

    internal static List<UsersActivityCount> GetMockUsersActivityCountResponseResult(int ExpectedResultValue)
    {
        List<UsersActivityCount> data = new();

        if (ExpectedResultValue == 1)
        {
            data.AddRange(
                new List<UsersActivityCount>()
                {
                    new UsersActivityCount()
                    {
                        UserId = 1,
                        NumberOfPosts = 2,
                        NumberofTodos = 1,
                    },
                    new UsersActivityCount()
                    {
                        UserId = 2,
                        NumberOfPosts = 1,
                        NumberofTodos = 3,
                    },
                    new UsersActivityCount()
                    {
                        UserId = 3,
                        NumberOfPosts = 1,
                        NumberofTodos = 4,
                    },
                }
            );
        }

        if (ExpectedResultValue == 2)
        {
            data.AddRange(
                new List<UsersActivityCount>()
                    {
                        new UsersActivityCount()
                        {
                            UserId = 1,
                            NumberOfPosts = 2,
                            NumberofTodos = 1,
                        },
                        new UsersActivityCount()
                        {
                            UserId = 3,
                            NumberOfPosts = 1,
                            NumberofTodos = 4,
                        }
                    });
        }

        if (ExpectedResultValue == 3)
        {
            data.Add(
                new UsersActivityCount()
                {
                    UserId = 1,
                    NumberOfPosts = 2,
                    NumberofTodos = 1,
                }
            );
        }

        return data;
    }

    private static void populateListTodos(this List<Todos> response, int id)
    {
        response.Add(new Todos
        {
            Id = id,
            UserId = id,
            Completed = false,
            Todo = $"todo{id}"
        });
    }
}
