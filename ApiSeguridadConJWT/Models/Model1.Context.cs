﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiSeguridadConJWT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class practica_seguridadEntities : DbContext
    {
        public practica_seguridadEntities()
            : base("name=practica_seguridadEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<estado> estado { get; set; }
        public virtual DbSet<historial_pagos> historial_pagos { get; set; }
        public virtual DbSet<municipio> municipio { get; set; }
        public virtual DbSet<tarjeta> tarjeta { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
    }
}