﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Registration_server_project.Entities;

public partial class LoginDetail
{
    public int LoginId { get; set; }

    public string FirstName { get; set; }

    public string City { get; set; }

    public int? RegisterId { get; set; }

    public virtual RegistrationDetail Register { get; set; }
}