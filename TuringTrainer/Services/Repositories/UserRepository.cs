using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class UserRepository : IUserRepository
{

    private readonly Supabase.Client client = Database.getClient();


    public ObservableCollection<User> Users { get; private set; } = new();

    public async Task SelectAllUsers()
    {
        try
        {
            var response = await client.From<User>().Get();
            Users.Clear();

            foreach (var user in response.Models)
            {
                Users.Add(user);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching users: {ex.Message}");
        }
    }

    public async Task SignUpUser(string email, string password)
    {
        var session = await client.Auth.SignUp(email, password);
    }

    public async Task SignInUser(string email, string password)
    {
        var session = await client.Auth.SignInWithPassword(email, password);
    }

    public async Task SignOutUser()
    {
        await client.Auth.SignOut();
    }

    

    public async Task SelectAllUsersByGroup(string groupId)
    {
        return;
    }

    public async Task SelectAllUsersByClass(string classId)
    {
        return;
    }

    public async Task SelectAllUsersBySport(string sportId)
    {
        return;
    }

    public async Task<User?> SelectUser(Guid userId)
    {
        var response = await client.From<User>().Where(user => user.UserId == userId).Get();
        if (response != null)
        {
            return response!.Models.FirstOrDefault();
        }
        return null;
    }

    public async Task<UserAdditionError> InsertUser(User user)
    {
        try
        {
            var response = await client.From<User>().Insert(user);
            if (response.Models.Any())
            {
                Users.Add(response.Models.First());
            }
            else
            {
                return UserAdditionError.DuplicateUserId;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Insert failed, {0}", ex);
            return ex.Message.Contains("already exists") ? UserAdditionError.DuplicateUserId : UserAdditionError.DBAdditionError;
        }
        return UserAdditionError.NoError;
    }

    public async Task<UserEditError> UpdateUser(User userToUpdate, String firstName, String middleInitial, String lastName, bool isCoach)
    {
        try
        {
            userToUpdate.FirstName = firstName;
            userToUpdate.MiddleInitial = middleInitial;
            userToUpdate.LastName = lastName;
            userToUpdate.IsCoach = isCoach;
            var response = await client.From<User>().Update(userToUpdate);
            if (response.Models.Any())
            {
                var updatedUser = response.Models.First();
                var index = Users.IndexOf(userToUpdate);
                if (index >= 0)
                {
                    Users[index] = updatedUser;
                }
            }
            else
            {
                return UserEditError.UserNotFound;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Update failed, {0}", ex);
            return UserEditError.DBEditError;
        }
        return UserEditError.NoError;
    }

    public async Task<UserDeletionError> DeleteUser(User userToDelete)
    {
        try
        {
            var response = await client.From<User>().Delete(userToDelete);
            if (response.Models.Any())
            {
                Console.WriteLine($"{Users.Count}");
                Users.Remove(userToDelete);
                Console.WriteLine($"{Users.Count}");
            }
            else
            {
                return UserDeletionError.UserNotFound;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Delete failed, {0}", ex);
            return UserDeletionError.DBDeletionError;
        }
        return UserDeletionError.NoError;
    }
}
