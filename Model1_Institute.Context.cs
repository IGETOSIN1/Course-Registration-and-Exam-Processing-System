﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web_Application_Higher_Institution
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities_Institute : DbContext
    {
        public Entities_Institute()
            : base("name=Entities_Institute")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<table_calendar_year> table_calendar_year { get; set; }
        public virtual DbSet<table_campus_add> table_campus_add { get; set; }
        public virtual DbSet<table_course_add> table_course_add { get; set; }
        public virtual DbSet<table_course_register> table_course_register { get; set; }
        public virtual DbSet<table_department_add> table_department_add { get; set; }
        public virtual DbSet<table_faculty_add> table_faculty_add { get; set; }
        public virtual DbSet<table_grading_system> table_grading_system { get; set; }
        public virtual DbSet<table_level_add> table_level_add { get; set; }
        public virtual DbSet<table_mode_add> table_mode_add { get; set; }
        public virtual DbSet<table_program_add> table_program_add { get; set; }
        public virtual DbSet<table_result> table_result { get; set; }
        public virtual DbSet<table_school_details> table_school_details { get; set; }
        public virtual DbSet<table_semester_add> table_semester_add { get; set; }
        public virtual DbSet<table_staff> table_staff { get; set; }
        public virtual DbSet<table_status_add> table_status_add { get; set; }
        public virtual DbSet<table_students> table_students { get; set; }
        public virtual DbSet<table_user> table_user { get; set; }
    }
}