using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security;
using System.Web;
using System.Web.Caching;
using TagPortal.Core;
using TagPortal.Core.Security;
using TagPortal.Core.Service;

namespace TbxPortal.Web.Infrastructure.Handlers
{
    public class ImageHandler : IHttpHandler
    {
        public int SmallX = 150;
        public int SmallY = 150;
        public int MediumX = 200;
        public int MediumY = 300;
        public int LargeX = 1720;
        public int largeY = 968;
        protected TagIdentity CurrentUser => (TagIdentity)TagPrincipal.Identity;
        protected ServiceContext Services => TagAppContext.Current.Services;
        private TagPrincipal TagPrincipal
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    throw new SecurityException("Not authenticated");
                if (!(HttpContext.Current.User is TagPrincipal))
                    throw new SecurityException("Invalid principal");

                return (TagPrincipal)HttpContext.Current.User;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                long attachmentId = 0;
                string type = "small";
                long.TryParse(context.Request.QueryString["id"], out attachmentId);
                type = context.Request.QueryString["size"] != null ? context.Request.QueryString["size"] : type;

                string query = context.Request["id"] + "-" + type;

                if (context.Cache[query] != null)
                {
                    //server side caching using asp.net caching
                    Bitmap image = (Bitmap)context.Cache[query];
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, ImageFormat.Png);
                        memoryStream.Position = 0;
                        context.Response.BinaryWrite(memoryStream.ToArray());
                    }
                    return;
                }

                {
                   
                  
                    {
                        var filePath = context.Server.MapPath("/Media/noArticle.jpg");
                        CreateImage(context, new MemoryStream(File.ReadAllBytes(filePath)), query, type);
                    }
                }



            }
            catch (Exception ex)
            {
                LogException(ex);

            }


        }

        public void CreateImage(HttpContext context, MemoryStream memoryStream, string query, string type)
        {
            using (MemoryStream imageStream = memoryStream)
            {
                using (Image img = Image.FromStream(imageStream))
                {
                    Bitmap newImage;
                    Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    double imageWidth = img.Width;
                    double imageHeight = img.Height;
                    double imageRatio = imageWidth / imageHeight;
                    double maxWidth = imageWidth;
                    double maxHeight = imageHeight;

                    switch (type)
                    {
                        case "small":
                            maxWidth = SmallX;
                            maxHeight = SmallY;
                            break;
                        case "medium":
                            maxWidth = MediumX;
                            maxHeight = MediumY;
                            break;
                        case "large":
                            maxWidth = LargeX;
                            maxHeight = largeY;
                            break;
                        default:
                            maxWidth = LargeX;
                            maxHeight = largeY;
                            break;
                    }
                    if (imageWidth > maxWidth)
                    {
                        imageWidth = maxWidth;
                        imageHeight = imageWidth / imageRatio;
                    }
                    if (imageHeight > maxHeight)
                    {
                        imageHeight = maxHeight;
                        imageWidth = imageHeight * imageRatio;
                    }

                    newImage = (Bitmap)img.GetThumbnailImage((int)imageWidth, (int)imageHeight, myCallback, IntPtr.Zero);

                    imageStream.Close();
                    imageStream.Dispose();

                    using (MemoryStream newImageStream = new MemoryStream())
                    {

                        newImage.Save(newImageStream, ImageFormat.Png);
                        newImageStream.Position = 0;

                        context.Response.Buffer = true;
                        context.Response.Clear();
                        context.Response.ContentType = "application/octet-stream";
                        context.Cache.Insert(query, newImage, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
                        context.Response.BinaryWrite(newImageStream.ToArray());
                        context.Response.Cache.SetCacheability(HttpCacheability.Public);
                        context.Response.Cache.SetExpires(DateTime.Now.AddMinutes(10));
                        context.Response.Cache.SetMaxAge(TimeSpan.FromMinutes(10));
                    }



                }

            }

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }


        }

        public bool ThumbnailCallback()
        {

            return true;
        }

        protected void LogException(Exception exception)
        {
            //Services.ErrorLogService.Insert(exception, CurrentUser.UserId, CurrentUser.Username, CurrentUser.Email);
        }

    }
}
