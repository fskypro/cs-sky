// ------------------------------------------------------------------
// Description : PList 图像文件阅读器
// Author      : hyw
// Date        : 2014.11.26
// Histories   :
// ------------------------------------------------------------------

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace XTreme.XTPList
{
	// ------------------------------------------------------------------------
	// FrameImage
	// ------------------------------------------------------------------------
	public class XTFrameInfo : XTFrame
	{
		private Image m_image;
		public XTFrameInfo(XTFrame frame, Image image)
			: base(frame)
		{
			this.m_image = image;
		}

		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		// 子纹理图像
		public Image Image { get { return this.m_image; } }
	}


	// ------------------------------------------------------------------------
	// XTPListImage
	// ------------------------------------------------------------------------
	public class XTPListImage
	{
		private Dictionary<string, XTFrameInfo> m_frames;
		private XTPListFile m_plist;
		private Image m_texture;

		private XTPListImage(XTPListFile plist, Image image, bool isCache)
		{
			this.m_plist = plist;
			this.m_texture = image;
			if (!isCache) { this.m_frames = null; }
			else { this.m_frames = new Dictionary<string, XTFrameInfo>(); }
		}

		//-----------------------------------------------------------
		// properties
		//-----------------------------------------------------------
		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		// 纹理格式
		public int Format { get { return this.m_plist.Format; } }

		// PList 文件路径
		public string FilePath { get { return this.m_plist.FilePath; } }

		// 纹理大小
		public Size TextureSize { get { return this.m_plist.TextureSize; } }

		// 纹理文件名
		public string TextureFileName { get { return this.m_plist.TextureFileName;} }

		// 纹理文件路径
		public string TextureFilePath { get { return this.m_plist.TextureFilePath; } }

		public Image Texture { get { return this.m_texture; } }


		//-----------------------------------------------------------
		// private
		//-----------------------------------------------------------
		private XTFrameInfo CreateFrameInfo(XTFrame frame)
		{
			Size size = frame.Frame.Size;
			Point pos = frame.Frame.Location;
			Rectangle dstRect = new Rectangle(0, 0, size.Width, size.Height);
			Bitmap frameImage = new Bitmap(size.Width, size.Height);
			Graphics g = Graphics.FromImage(frameImage);
			g.DrawImage(this.m_texture, dstRect, pos.X, pos.Y, size.Width, size.Height, GraphicsUnit.Pixel);
			g.Dispose();
			//if (frame.Rotated)
			//	frameImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
			return new XTFrameInfo(frame, frameImage);
		}

		//-----------------------------------------------------------
		// public
		//-----------------------------------------------------------
		// 如果 PList 文件格式不正确，则抛出：XTInvalidPListFileException 异常 
		// 如果加载图片资源失败，则抛出 XTNotFoundPListImageFileException 异常
		public static XTPListImage FromFile(string file, bool isCache=true)
		{
			XTPListFile plfile = XTPListFile.Create(file, isCache);
			if (plfile == null) return null;
			Image image;
			try { image = Image.FromFile(plfile.TextureFilePath); }
			catch { throw new XTNotFoundPListImageFileException(file, plfile.TextureFilePath); }
			return new XTPListImage(plfile, image, isCache);
		}

		// -------------------------------------------
		public XTFrameInfo GetFrameInfo(string name)
		{
			XTFrame frame = this.m_plist.GetFrame(name);
			if (frame == null) return null;
			if (this.m_frames == null)
			{
				return this.CreateFrameInfo(frame);
			}
			XTFrameInfo frameImage;
			if (!this.m_frames.TryGetValue(name, out  frameImage))
			{
				frameImage = this.CreateFrameInfo(frame);
				this.m_frames[name] = frameImage;
			}
			return frameImage;
		}

		public IEnumerable<XTFrameInfo> IterFrameInfos()
		{
 			if (this.m_frames == null)
			{
				foreach (XTFrame frame in this.m_plist.IterFrames())
				{
					yield return this.CreateFrameInfo(frame);
				}
			}
			else
			{
				XTFrameInfo frameImage;
				foreach (XTFrame frame in this.m_plist.IterFrames())
				{
					if (!this.m_frames.TryGetValue(frame.Name, out frameImage))
					{
						frameImage = this.CreateFrameInfo(frame);
						this.m_frames[frame.Name] = frameImage;
					}
					yield return frameImage;
				}
			}
		}

		public int GetFrameCount()
		{
			return this.m_plist.GetFrameCount();
		}
	}
}
