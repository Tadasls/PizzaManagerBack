using CompetitionEventsManager.Models;
using CompetitionEventsManager.Services.Adapters;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace CompetitionEventsManager.Data.InitialData
{
    /// <summary>
    /// that initial data
    /// </summary>
    public static class PizzaInitialData
    {
        public static readonly Pizza[] PizzaDataSeed = new Pizza[] {
      new Pizza() { PizzaID = 1, PizzaName = "A Pizza", Size = "small", Toppings = 2 },
        new Pizza() { PizzaID = 2, PizzaName = "B Pizza", Size = "medium", Toppings = 3 },
        new Pizza() { PizzaID = 3, PizzaName = "C Pizza", Size = "large", Toppings = 4 },


    };

        //public static readonly Order[] OrderDataSeed = new Order[] {
        //    new Order
        //    {
        //        OrderID = 1,
        //        OrderDate = DateTime.Now,
        //        Pizzas = new List<Pizza>
        //        {
        //            new Pizza
        //            {
        //                PizzaName = "Pepperoni",
        //                Size = "Large",
        //                Toppings = 2,
        //                Price = 10.99m
        //            },
        //            new Pizza
        //            {
        //                PizzaName = "Margherita",
        //                Size = "Medium",
        //                Toppings = 1,
        //                Price = 8.99m
        //            }
        //        }
        //    }
        //};
    }
}