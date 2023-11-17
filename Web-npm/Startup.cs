using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Web.Infrastructure.Filters;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/");
                options.Conventions.AllowAnonymousToPage("/login");
            });

            //not sure about this part !!
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilterAsync));
                options.Filters.Add(typeof(CommitLogFilterAsync));
                options.Filters.Add(typeof(UserClaimUpdateFilterAsync));
            }).AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
                option.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
                option.JsonSerializerOptions.AllowTrailingCommas = true;
                option.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                //option.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            }).ConfigureApiBehaviorOptions(option =>
            {
                option.SuppressModelStateInvalidFilter = true;
            }); 

            services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
            });

            Core.AppContext.Current = new Core.AppContext(
                sqlConnectionString: Configuration.GetConnectionString("Budgeting"),
                siteLogoByte: "",
                siteName: Configuration.GetValue<string>("System:SiteName"),
                siteUrl: Configuration.GetValue<string>("System:SiteUrl"),
                siteUrlFriendly: Configuration.GetValue<string>("System:SiteUrlFriendly"),
                resetPasswordUrl: Configuration.GetValue<string>("System:ResetPasswordUrl"),
                clientImageDirectory: Configuration.GetValue<string>("System:ClientImageDirectory"),

                noReplyAddress: Configuration.GetValue<string>("System:NoReplyAddress"),
                smtpDeliveryMethod: Configuration.GetValue<System.Net.Mail.SmtpDeliveryMethod>("MailSettings:DeliveryMethod"),
                smtpHost: Configuration.GetValue<string>("MailSettings:Host"),
                smtpPort: Configuration.GetValue<int>("MailSettings:port")
                );
            Infrastructure.AutoMapper.MapperConfiguration.InitAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();

            });
        }
    }
}
