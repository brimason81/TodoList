//  TODO:  BUG FIX ON DELETE - ID CHECK NOT QUITE RIGHT
//                           - COULD BE BECAUSE EDIT WILL DUPLICATE AN ITEM IF CHECKED MULITPLE TIMES
//

using TodoList.Data;
using TodoList.store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => { 
    
});
builder.Services.AddSingleton<ITodoStore>(new TodoStore());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();
