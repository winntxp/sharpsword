/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/9 9:57:43
 * ****************************************************************/
using System;
using System.IO;

namespace SharpSword.SDK
{
    /// <summary>
    /// 上传文件封装类
    /// </summary>
    public class FileItem
    {
        private string _fileName;
        private string _mimeType;
        private byte[] _content;
        private FileInfo fileInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        public FileItem(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
            {
                throw new ArgumentException("fileInfo is null or not exists!");
            }
            this.fileInfo = fileInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public FileItem(string filePath)
            : this(new FileInfo(filePath))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public FileItem(string fileName, byte[] content)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (content == null || content.Length == 0)
            {
                throw new ArgumentNullException(nameof(content));
            }
            this._fileName = fileName;
            this._content = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <param name="mimeType"></param>
        public FileItem(string fileName, byte[] content, string mimeType)
            : this(fileName, content)
        {
            if (string.IsNullOrEmpty(mimeType))
            {
                throw new ArgumentNullException(nameof(mimeType));
            }
            this._mimeType = mimeType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            if (this._fileName == null && this.fileInfo != null && this.fileInfo.Exists)
            {
                this._fileName = this.fileInfo.FullName;
            }
            return this._fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetMimeType()
        {
            return this._mimeType ?? (this._mimeType = Utils.GetMimeType(this.GetContent()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetContent()
        {
            if (this._content == null && this.fileInfo != null && this.fileInfo.Exists)
            {
                using (Stream stream = this.fileInfo.OpenRead())
                {
                    this._content = new byte[stream.Length];
                    stream.Read(this._content, 0, this._content.Length);
                }
            }
            return this._content;
        }
    }
}
