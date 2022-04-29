//.NET
global using System;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Diagnostics;
global using System.Diagnostics;
global using System.Collections.Generic;
global using System.Linq;

//Custom
global using Project.Configuration;
global using Project.Context;
global using Project.Repositories;
global using Project.Model;
global using Project.Service;
global using Project.GraphQL;
global using Project.Validator;

//NuGet
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;
global using FluentValidation;
global using FluentValidation.AspNetCore;