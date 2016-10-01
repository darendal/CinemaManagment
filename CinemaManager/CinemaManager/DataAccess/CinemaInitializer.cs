﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.DataAccess
{
    public class CinemaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CinemaContext>
    {
        protected override void Seed(CinemaContext context)
        {
            var cinemas = new List<Cinema>()
            {
                new Cinema() {Name="Cinema1", Address="123 Fake Street" }

            };
            cinemas.ForEach(c => context.Cinemas.Add(c));
            context.SaveChanges();

            var theaters = new List<Theater>()
            {
                new Theater() {CinemaId=1 },
                new Theater() {CinemaId=1 },
                new Theater() {CinemaId=1 }
            };

            theaters.ForEach(t => context.Theaters.Add(t));
            context.SaveChanges();

        }
    }
}
