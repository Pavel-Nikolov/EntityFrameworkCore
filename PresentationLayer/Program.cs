using ServiceLayer.CRUDManager;
using ServiceLayer.Enums;
using ServiceLayer.Events;
using ServiceLayer.Manipulations;
using System;
using System.Linq;

namespace PresentationLayer
{
    class Program
    {
        static void SetUp()
        {
            EventManager.ShowBrand += PrintManager.PrintBrand;
            EventManager.ShowProduct += PrintManager.PrintProduct;
            EventManager.ShowUser += PrintManager.PrintUsers;
        }
        static void Main()
        {
            SetUp();           
            string[] input;
            string[] command;
            while (true)
            {
                FiltrationContext filtrationContext = null;
                OrderingContext orderingContext = null;
                try
                {
                    input = Console.ReadLine().Split(',');
                    command = input[0].Split();
                    string[] subcommand;
                    if (command[1].ToLower() == "readall" || command[2] == "find")
                    {
                        foreach (var item in input.Skip(1))
                        {
                            subcommand = item.Trim().Split();
                            switch (subcommand[0].ToLower())
                            {
                                case "order":
                                    string orderingProperty = subcommand[1];
                                    bool asscending = bool.Parse(subcommand[2]);
                                    orderingContext = new OrderingContext(orderingProperty, asscending);
                                    break;
                                case "filter":
                                    string filteringProperty = subcommand[1];
                                    object value = (object)subcommand[2];
                                    filtrationContext = new FiltrationContext(filteringProperty, value);
                                    break;
                                default:
                                    break;
                            }

                        }

                    }

                    ManipulationContext manipulationContext = new ManipulationContext(filtrationContext,orderingContext);
                    switch (command[0].ToLower())
                    {
                        case "brand":
                            switch (command[1].ToLower())
                            {
                                case "create":
                                    DBManager.RunCommand(EntityType.Brand, OperationType.Create, manipulationContext, command[2], command.Skip(3).ToArray());
                                    break;
                                case "read":
                                    DBManager.RunCommand(EntityType.Brand, OperationType.Read, manipulationContext,  command[2]);
                                    break;
                                case "readall":
                                    DBManager.RunCommand(EntityType.Brand, OperationType.ReadAll, manipulationContext);
                                    break;
                                case "delete":
                                    DBManager.RunCommand(EntityType.Brand, OperationType.Delete, manipulationContext, command[2]);
                                    break;
                                case "update":
                                    DBManager.RunCommand(EntityType.Brand, OperationType.Update,manipulationContext, command[2], command[3], command.Skip(4).ToArray());
                                    break;
                                case "find":
                                    DBManager.RunCommand(EntityType.Brand, OperationType.Find,manipulationContext, command[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid operation");
                                    break;
                            }
                            break;

                        case "product":
                            switch (command[1].ToLower())
                            {
                                case "create":
                                    DBManager.RunCommand(EntityType.Product, OperationType.Create,manipulationContext, command[2], command[3], command[4], command[5], command[6], command.Skip(7).Select(int.Parse).ToArray());
                                    break;
                                case "read":
                                    DBManager.RunCommand(EntityType.Product, OperationType.Read,manipulationContext, command[2]);
                                    break;
                                case "readall":
                                    DBManager.RunCommand(EntityType.Product, OperationType.ReadAll,manipulationContext);
                                    break;
                                case "update":
                                    DBManager.RunCommand(EntityType.Product, OperationType.Update, manipulationContext, command[2], command[3], command[4], command[5], command[6], command.Skip(7).Select(int.Parse).ToArray());
                                    break;
                                case "delete":
                                    DBManager.RunCommand(EntityType.Product, OperationType.Delete,manipulationContext, command[2]);
                                    break;
                                case "find":
                                    DBManager.RunCommand(EntityType.Product, OperationType.Find,manipulationContext, command[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid operation");
                                    break;
                            }
                            break;

                        case "user":
                            switch (command[1].ToLower())
                            {
                                case "create":
                                    DBManager.RunCommand(EntityType.User, OperationType.Create,manipulationContext, command[2], command[3], command.Skip(4).ToArray());
                                    break;
                                case "read":
                                    DBManager.RunCommand(EntityType.User, OperationType.Read,manipulationContext, command[2]);
                                    break;
                                case "readall":
                                    DBManager.RunCommand(EntityType.User, OperationType.ReadAll, manipulationContext);
                                    break;
                                case "update":
                                    DBManager.RunCommand(EntityType.User, OperationType.Update,manipulationContext, command[2], command[3], command[4], command.Skip(5).ToArray());
                                    break;
                                case "delete":
                                    DBManager.RunCommand(EntityType.User, OperationType.Delete,manipulationContext, command[2]);
                                    break;
                                case "find":
                                    DBManager.RunCommand(EntityType.User, OperationType.Find,manipulationContext, command[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid operation");
                                    break;
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid type");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Next Command");
                }
            }
        }
    }
}
