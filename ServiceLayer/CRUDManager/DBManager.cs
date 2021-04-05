﻿using DomainLayer.DataAccess;
using DomainLayer.Models;
using ServiceLayer.Enums;
using ServiceLayer.Events;
using ServiceLayer.Manipulations;
using ServiceLayer.Manipulations.PropertyGetters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.CRUDManager
{
    public static class DBManager
    {
        private static Context context = new Context();
        private static readonly BrandRepository brandRepository;
        private static readonly ProductRepository productRepository;
        private static readonly UserRepository userRepository;
        private static readonly DBFinder finder;
        private static BrandPropertyGetter brandPropertyGetter;
        private static ProductPropertyGetter productPropertyGetter;
        private static UserPropertyGetter userPropertyGetter;
        

        static DBManager()
        {
            brandRepository = new BrandRepository(context);
            productRepository = new ProductRepository(context);
            userRepository = new UserRepository(context);
            finder = new DBFinder(context);
        }

        public static void RunCommand(EntityType entityType, OperationType operationType, ManipulationContext manipulationContext ,params object[] args)
        {
            context = new Context();
            switch (entityType)
            {
                case EntityType.Brand:
                    ManageBrands(operationType,manipulationContext, args);
                    break;
                case EntityType.Product:
                    ManageProducts(operationType,manipulationContext, args);
                    break;
                case EntityType.User:
                    ManageUsers(operationType, manipulationContext, args);
                    break;
                default:
                    break;
            }
        }

        private static void ManageUsers(OperationType operationType, ManipulationContext manipulationContext, object[] args)
        {
            switch (operationType)
            {
                case OperationType.Create:
                    List<Product> productsToBeCreated = new List<Product>();
                    if (args.Length > 2)
                    {
                        string[] productIdsToBeCreated = args[2] as string[];
                        productsToBeCreated = finder.GetProducts(productIdsToBeCreated);
                    }
                    User userToBeCreated = EntityFactory.GenerateUser(default(int), args[0], args[1], productsToBeCreated);
                    userRepository.Create(userToBeCreated);
                    break;

                case OperationType.Read:
                    int readKey = int.Parse(args[0].ToString());
                    User readUser = userRepository.Read(readKey);
                    EventManager.OnUserShowing(readUser);
                    break;

                case OperationType.ReadAll:
                    ICollection<User> usersRead = userRepository.ReadAll();
                    if (manipulationContext.FiltrationContext != null)
                    {
                        userPropertyGetter = new UserPropertyGetter(manipulationContext.FiltrationContext.PropertyName);
                        usersRead = Manipulate<User, int>.Filter(usersRead, userPropertyGetter, manipulationContext.FiltrationContext.Value);
                    }
                    if (manipulationContext.OrderingContext != null)
                    {
                        userPropertyGetter = new UserPropertyGetter(manipulationContext.OrderingContext.PropertyName);
                        usersRead = Manipulate<User, int>.OrderBy(usersRead, userPropertyGetter, manipulationContext.OrderingContext.Acssending);
                    }
                    
                    foreach (var item in usersRead)
                    {
                        EventManager.OnUserShowing(item);
                    }
                    break;

                case OperationType.Delete:
                    int deleteKey = int.Parse(args[0].ToString());
                    userRepository.Delete(deleteKey);
                    break;

                case OperationType.Update:
                    List<Product> productsToBeUpdated = new List<Product>();
                    if (args.Length > 3)
                    {
                        string[] productIdsToBeUpdated = args[3] as string[];
                        productsToBeUpdated = finder.GetProducts(productIdsToBeUpdated);
                    }
                    User userToBeUpdated = EntityFactory.GenerateUser(args[0], args[1], args[2], productsToBeUpdated);
                    userRepository.Update(userToBeUpdated);
                    break;
                case OperationType.Find:
                    string index = args[0].ToString();
                    ICollection<User> usersFound = userRepository.Find(index);
                    if (manipulationContext.FiltrationContext != null)
                    {
                        userPropertyGetter = new UserPropertyGetter(manipulationContext.FiltrationContext.PropertyName);
                        usersFound = Manipulate<User, int>.Filter(usersFound, userPropertyGetter, manipulationContext.FiltrationContext.Value);
                    }
                    if (manipulationContext.OrderingContext!=null)
                    {
                        userPropertyGetter = new UserPropertyGetter(manipulationContext.OrderingContext.PropertyName);
                        usersFound = Manipulate<User, int>.OrderBy(usersFound, userPropertyGetter, manipulationContext.OrderingContext.Acssending);
                    }
                    
                    foreach (var item in usersFound)
                    {
                        EventManager.OnUserShowing(item);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Converts string to int?
        /// </summary>
        /// <param name="input">The string to be converted</param>
        /// <param name="nullSrting">The representation of the null value</param>
        /// <returns>A int? of value of the corresponding number or null if it is the nullString</returns>
        /// <exception cref="FormatException">The number is not a number or the null string</exception>
        
        public static int? ParseNullable(string input, string nullSrting)
        {
            if (input == nullSrting)
            {
                return null;
            }
            return int.Parse(input);
        }

        private static void ManageProducts(OperationType operationType, ManipulationContext manipulationContext, object[] args)
        {
            switch (operationType)
            {
                case OperationType.Create:
                    int brandToBeCreatedId = int.Parse(args[4].ToString());
                    Brand brandToBeCreated = finder.GetBrand(brandToBeCreatedId);
                    List<User> usersToBeCreated = new List<User>();

                    if (args.Length > 5)
                    {
                        int[] userIdsToBeCreated = args[5] as int[];
                        usersToBeCreated = finder.GetUsers(userIdsToBeCreated);
                    }

                    Product productToBeCreated = EntityFactory.GenerateProduct(args[0], args[1], args[2], args[3],
                                                                               brandToBeCreated, usersToBeCreated);

                    productRepository.Create(productToBeCreated);
                    break;

                case OperationType.Read:
                    string readKey = args[0].ToString();
                    Product readProduct = productRepository.Read(readKey);
                    EventManager.OnProductShowing(readProduct);
                    break;

                case OperationType.ReadAll:
                    ICollection<Product> readProducts = productRepository.ReadAll();
                    if (manipulationContext.FiltrationContext != null)
                    {
                        productPropertyGetter = new ProductPropertyGetter(manipulationContext.FiltrationContext.PropertyName);
                        readProducts = Manipulate<Product, string>.Filter(readProducts, productPropertyGetter, manipulationContext.FiltrationContext.Value);
                    }
                    if (manipulationContext.OrderingContext!=null)
                    {
                        productPropertyGetter = new ProductPropertyGetter(manipulationContext.OrderingContext.PropertyName);
                        readProducts = Manipulate<Product, string>.OrderBy(readProducts, productPropertyGetter, manipulationContext.OrderingContext.Acssending);
                    }
                    
                    foreach (var item in readProducts)
                    {
                        EventManager.OnProductShowing(item);
                    }
                    break;

                case OperationType.Delete:
                    string deleteKey = args[0].ToString();
                    productRepository.Delete(deleteKey);
                    break;

                case OperationType.Update:
                    int brandToBeUpdatedId = int.Parse(args[4].ToString());
                    Brand brandToBeUpdated = finder.GetBrand(brandToBeUpdatedId);
                    List<User> usersToBeUpdated = new List<User>();

                    if (args.Length > 5)
                    {
                        int[] userIdsToBeUpdated = args[5] as int[];
                        usersToBeUpdated = finder.GetUsers(userIdsToBeUpdated);
                    }

                    Product productToBeUpdated = EntityFactory.GenerateProduct(args[0], args[1], args[2], args[3],
                                                                               brandToBeUpdated, usersToBeUpdated);
                    productRepository.Update(productToBeUpdated);
                    break;

                case OperationType.Find:
                    string index = args[0].ToString();
                    ICollection<Product> productsFound = productRepository.Find(index);
                    if (manipulationContext.FiltrationContext != null)
                    {
                        productPropertyGetter = new ProductPropertyGetter(manipulationContext.FiltrationContext.PropertyName);
                        productsFound = Manipulate<Product, string>.Filter(productsFound, productPropertyGetter, manipulationContext.FiltrationContext.Value);
                    }
                    if (manipulationContext.OrderingContext!=null)
                    {
                        productPropertyGetter = new ProductPropertyGetter(manipulationContext.OrderingContext.PropertyName);
                        productsFound = Manipulate<Product, string>.OrderBy(productsFound, productPropertyGetter, manipulationContext.OrderingContext.Acssending);
                    }
                    
                    foreach (var item in productsFound)
                    {
                        EventManager.OnProductShowing(item);
                    }
                    break;
                default:
                    break;
            }
        }

        private static void ManageBrands(OperationType operationType, ManipulationContext manipulationContext, object[] args)
        {
            switch (operationType)
            {
                case OperationType.Create:
                    List<Product> productsToBeCreated = new List<Product>();

                    if (args.Length > 1)
                    {
                        string[] productsIdsToBeCreated = args[1] as string[];
                        productsToBeCreated = finder.GetProducts(productsIdsToBeCreated);
                    }

                    Brand brandToBeCreated = EntityFactory.GenerateBrand(default(int), args[0], productsToBeCreated);

                    brandRepository.Create(brandToBeCreated);
                    break;

                case OperationType.Read:
                    int readKey = int.Parse(args[0].ToString());
                    Brand readBrand = brandRepository.Read(readKey);
                    EventManager.OnBrandShowing(readBrand);
                    break;

                case OperationType.ReadAll:
                    ICollection<Brand> readBrands = brandRepository.ReadAll();
                    if (manipulationContext.FiltrationContext!=null)
                    {
                        brandPropertyGetter = new BrandPropertyGetter(manipulationContext.FiltrationContext.PropertyName);
                        readBrands = Manipulate<Brand, int>.Filter(readBrands, brandPropertyGetter, manipulationContext.FiltrationContext.Value);
                    }
                    if (manipulationContext.OrderingContext!=null)
                    {
                        brandPropertyGetter = new BrandPropertyGetter(manipulationContext.OrderingContext.PropertyName);
                        readBrands = Manipulate<Brand, int>.OrderBy(readBrands, brandPropertyGetter, manipulationContext.OrderingContext.Acssending);
                    }
                    
                    foreach (var item in readBrands)
                    {
                        EventManager.OnBrandShowing(item);
                    }
                    break;

                case OperationType.Delete:
                    int deleteKey = int.Parse(args[0].ToString());
                    brandRepository.Delete(deleteKey);
                    break;

                case OperationType.Update:
                    List<Product> productsToBeUpdated = new List<Product>();
                    if (args.Length > 2)
                    {
                        string[] productsIdsToBeUpdated = args[2] as string[];
                        productsToBeUpdated = finder.GetProducts(productsIdsToBeUpdated);
                    }
                    Brand moviesToBeUpdated = EntityFactory.GenerateBrand(args[0], args[1], productsToBeUpdated);
                    brandRepository.Update(moviesToBeUpdated);
                    break;

                case OperationType.Find:
                    string index = args[0].ToString();
                    ICollection<Brand> brandsFound = brandRepository.Find(index);
                    if (manipulationContext.FiltrationContext != null)
                    {
                        brandPropertyGetter = new BrandPropertyGetter(manipulationContext.FiltrationContext.PropertyName);
                        brandsFound = Manipulate<Brand, int>.Filter(brandsFound, brandPropertyGetter, manipulationContext.FiltrationContext.Value);
                    }
                    if (manipulationContext.OrderingContext!=null)
                    {
                        brandPropertyGetter = new BrandPropertyGetter(manipulationContext.OrderingContext.PropertyName);
                        brandsFound = Manipulate<Brand, int>.OrderBy(brandsFound, brandPropertyGetter, manipulationContext.OrderingContext.Acssending);
                    }
                    
                    foreach (var item in brandsFound)
                    {
                        EventManager.OnBrandShowing(item);
                    }
                    break;
                default:
                    break;
            }
        }





        
    }
}