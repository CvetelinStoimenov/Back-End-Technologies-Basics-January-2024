﻿using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Business;
using LibroConsoleAPI.IntegrationTests;
using LibroConsoleAPI.Repositories;

public class BookManagerFixture : IDisposable
{
    public TestLibroDbContext DbContext { get; private set; }
    public IBookManager BookManager { get; private set; }

    public BookManagerFixture()
    {
        string dbName = $"TestDb_{Guid.NewGuid()}";
        DbContext = new TestLibroDbContext(dbName);
        var bookRepository = new BookRepository(DbContext);
        BookManager = new BookManager(bookRepository);
    }

    public void Dispose()
    {
        DbContext?.Dispose();
    }
}
