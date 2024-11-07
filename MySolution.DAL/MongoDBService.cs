using MongoDB.Driver;
using MySolution.DAL;
using Microsoft.Extensions.Options;
using System;

namespace MySolution.Services
{
    public class StudentService : IDisposable
    {
        private bool _disposed = false;

        private readonly IMongoCollection<Student> _studentsCollection;
        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient
        mongoClient)
        {
            var mongoDatabase =
                mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<Student>("Students");
        }
        public async Task<List<Student>> GetAsync() =>
            await _studentsCollection.Find(s => true).ToListAsync();
        public async Task<Student> GetByIdAsync(int id) =>
            await _studentsCollection.Find(s => s.StudentId == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Student student)
        {
            try
                {
                    Console.WriteLine($"Inserting student: {student.FirstName} {student.LastName}");
                    await _studentsCollection.InsertOneAsync(student);
                    Console.WriteLine("Insert successful.");
                }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting student: {ex.Message}");
                throw;
            }
        }
        
        public async Task UpdateAsync(int id, Student updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(s => s.StudentId == id, updatedStudent);
        public async Task RemoveAsync(int id) =>
            await _studentsCollection.DeleteOneAsync(s => s.StudentId == id);

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}