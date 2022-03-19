using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BookShop;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

// ReSharper disable CheckNamespace, InconsistentNaming

[TestFixture]
public class ModelsCreationTest
{
    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateModel()
    {
        var context = GetType("BookShopContext");

        var dbSetData = new[]
        {
                new DbSetInfo("Authors", "Author", "Id FirstName LastName Phone Email AuthorsBooks".Split()),
                new DbSetInfo("AuthorsBooks", "AuthorBook", "AuthorId Author BookId Book".Split()),
                new DbSetInfo("Books","Book", "Id Name Genre Price Pages PublishedOn AuthorsBooks".Split()),
            };

        foreach (var dbSetInfo in dbSetData)
        {
            ValidateDbSet(context, dbSetInfo);
        }
    }

    private static void ValidateDbSet(Type context, DbSetInfo info)
    {
        var expectedDbSetType = GetDbSetType(info.EntityType);

        AssertCollectionIsOfType(context, info.DbSetName, expectedDbSetType);

        var modelType = GetType(info.EntityType);

        foreach (var property in info.Properties)
        {
            var propertyType = GetPropertyByName(modelType, property);

            var errorMessage = $"{modelType.Name}.{property} property does not exist!";
            Assert.IsNotNull(propertyType, errorMessage);
        }
    }

    private static PropertyInfo GetPropertyByName(Type type, string propName)
    {
        var properties = type.GetProperties();

        var firstOrDefault = properties.FirstOrDefault(p => p.Name == propName);

        return firstOrDefault;
    }

    private static void AssertCollectionIsOfType(Type type, string propertyName, Type collectionType)
    {
        var property = GetPropertyByName(type, propertyName);

        var errorMessage = string.Format($"{type.Name}.{propertyName} property not found!");

        Assert.IsNotNull(property, errorMessage);

        Assert.That(collectionType.IsAssignableFrom(property.PropertyType));
    }

    private static Type GetDbSetType(string modelName)
    {
        var modelType = GetType(modelName);

        var dbSetType = typeof(DbSet<>).MakeGenericType(modelType);
        return dbSetType;
    }

    private static Type GetType(string typeName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == typeName);

        Assert.IsNotNull(modelType, $"{typeName} not found!");

        return modelType;
    }

    private class DbSetInfo
    {
        public DbSetInfo(string dbSetName, string entityType, IEnumerable<string> properties)
        {
            this.DbSetName = dbSetName;
            this.EntityType = entityType;
            this.Properties = properties;
        }

        public string DbSetName { get; }

        public string EntityType { get; }

        public IEnumerable<string> Properties { get; }
    }
}