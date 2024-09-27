﻿using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProductDAO
{
    public static List<Product> GetProducts()
    {
        var listProducts = new List<Product>();
        try
        {
            using (var context = new MyStoreContext())
            {
                listProducts = context.Products.Include(p=>p.Category).ToList();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return listProducts;
    }

    public static Product FindProductById(int prodId)
    {
        var p = new Product();
        try
        {
            using (var context = new MyStoreContext())
            {
                p = context.Products.Include(p=>p.Category).SingleOrDefault(x => x.ProductId == prodId);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return p;
    }

    public static void SaveProduct(Product p)
    {
        try
        {
            using (var context = new MyStoreContext())
            {
                context.Products.Add(p);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public static void UpdateProduct(Product p)
    {
        try
        {
            using (var context = new MyStoreContext())
            {
                context.Entry(p).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public static void DeleteProduct(Product p)
    {
        try
        {
            using (var context = new MyStoreContext())
            {
                var p1 = context.Products.SingleOrDefault(c => c.ProductId == p.ProductId);
                context.Products.Remove(p1);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}