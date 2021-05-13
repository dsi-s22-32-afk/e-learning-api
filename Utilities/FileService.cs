using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Entities;
using Microsoft.AspNetCore.Http;
using UniWall.Uploads;
using UniWall.Exceptions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using UniWall.Data.Contexts;

namespace UniWall.Utilities
{
    public class FileService
    {
        private readonly UploadConfig _config;
        private readonly IWebHostEnvironment _env;
        private readonly ApiDbContext _db;

        public FileService(UploadConfig config, IWebHostEnvironment env, ApiDbContext db)
        {
            _config = config;
            _env = env;
            _db = db;
        }

        public async Task<UploadedFile> ProcessFile(IFormFile uploadedFile)
        {
            if (uploadedFile.Length > _config.MaxSize)
            {
                throw new HttpException(400, new ApiException("FILE.001", "The file exceeds the allowed size"));
            }
            if (!_config.AllowedMimeTypes.Contains(uploadedFile.ContentType))
            {
                throw new HttpException(400, new ApiException("FILE.002", "The file has a forbidden type"));
            }

            string fileDir = Path.Combine(_env.ContentRootPath, _config.DestinationDir);
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadedFile.FileName);
            string filePath = Path.Combine(fileDir, fileName);
            FileStream stream = File.Create(filePath);

            await uploadedFile.CopyToAsync(stream);

            UploadedFile file = new()
            {
                Directory = fileDir,
                FileName = fileName,
                MimeType = uploadedFile.ContentType,
                Size = uploadedFile.Length
            };

            await _db.AddAsync(file);
            await _db.SaveChangesAsync();

            return file;
        }
    }
}
