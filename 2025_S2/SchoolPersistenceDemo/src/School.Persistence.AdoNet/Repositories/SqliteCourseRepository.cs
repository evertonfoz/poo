using System;
using System.Data;
using School.Domain.Domain.Repositories;
using School.Domain.Entities;
using School.Persistence.AdoNet.Connections;

namespace School.Persistence.AdoNet.Repositories;

public class SqliteCourseRepository(SqliteConnectionFactory
            connectionFactory) : ICourseRepository
{
    private readonly SqliteConnectionFactory _connectionFactory = connectionFactory ??
                throw new ArgumentNullException(nameof(connectionFactory));

    public Course Add(Course courseEntity)
    {
        ArgumentNullException.ThrowIfNull(courseEntity);

        using var connection = _connectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            
            command.CommandText =
                "INSERT INTO Courses (Name, WorkloadHours, IsActive) " +
                "VALUES (@Name, @WorkloadHours, @IsActive); " +
                "SELECT last_insert_rowid();";

            var pName = command.CreateParameter();
            pName.ParameterName = "@Name";
            pName.DbType = DbType.String;
            pName.Value = courseEntity.Name;
            command.Parameters.Add(pName);

            var pWorkload = command.CreateParameter();
            pWorkload.ParameterName = "@WorkloadHours";
            pWorkload.DbType = DbType.Int32;
            pWorkload.Value = courseEntity.WorkloadHours;
            command.Parameters.Add(pWorkload);

            var pIsActive = command.CreateParameter();
            pIsActive.ParameterName = "@IsActive";
            pIsActive.DbType = DbType.Int32;
            pIsActive.Value = courseEntity.IsActive ? 1 : 0;
            command.Parameters.Add(pIsActive);

            var result = command.ExecuteScalar();
            if (result is long id)
            {
                courseEntity.CourseId = (int)id;
            }

            return courseEntity;
    }

    public IReadOnlyList<Course> Find(Func<Course, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Course? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Course> ListAll()
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Course entity)
    {
        throw new NotImplementedException();
    }
}
