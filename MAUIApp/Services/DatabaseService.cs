using SQLite;
using MAUIApp.Models;

namespace MAUIApp.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    public async Task InitializeAsync()
    {
        if (_database is not null) return;

        _database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "users.db"));
        await _database.CreateTableAsync<User>();
    }

    public async Task<bool> RegisterUserAsync(string name, string email, string password)
    {
        await InitializeAsync();
        
        var existingUser = await _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        if (existingUser != null) return false;

        var user = new User { Name = name, Email = email, Password = password };
        await _database.InsertAsync(user);
        return true;
    }

    public async Task<bool> LoginUserAsync(string email, string password)
    {
        await InitializeAsync();
        
        var user = await _database.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        return user != null;
    }
}