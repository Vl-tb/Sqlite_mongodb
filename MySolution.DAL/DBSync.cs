using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;
using MySolution.Services;
using MySolution.DAL;
public class UpdateService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public UpdateService(IServiceScopeFactory serviceScopeFactory)
    
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using (var scope = _serviceScopeFactory.CreateAsyncScope())
        using (var mongoService =
            scope.ServiceProvider.GetRequiredService<StudentService>())
        using (var dbContext = scope.ServiceProvider.GetRequiredService<LabDbContext>())
        using (var transaction = new
            TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var students = dbContext.Students.ToList();
                foreach (var student in students)
                {
                    var studentDto = new Student()
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        DateOfBirth = student.DateOfBirth
                    };
                    var Modulus = 12345678;
                    studentDto.StudentId = (int)(long.Parse((GenerateNameHash(student.FirstName + student.LastName) % Modulus).ToString("D5") + (long.Parse(student.DateOfBirth.ToString("yyyyMMdd")) % Modulus).ToString()) % Modulus);

                    await mongoService.CreateAsync(studentDto);
                }
                transaction.Complete();
            }
            catch(Exception ex)
            {
                transaction.Dispose();
                throw;
            }
        }
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public static int GenerateNameHash(string input)
    {
        int hash = 0;
        foreach (char c in input)
        {
            hash = (hash * 31 + c) % 10000;
        }
        return Math.Abs(hash);
    }
}