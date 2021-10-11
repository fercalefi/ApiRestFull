using ApiRestFull.Data.VO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFull.Business
{
    public interface IFileBusiness
    {
        public byte[] Getfile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
