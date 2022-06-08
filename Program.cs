﻿using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      builder =>
//                      {
//                          builder.WithOrigins("https://qdee.ca");
//                      });
//});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(40);
});

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NkgjjmConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseMvc();
app.UseMvcWithDefaultRoute();
app.UseAuthorization();

app.MapRazorPages();



app.Run();
