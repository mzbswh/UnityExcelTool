using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
	public struct Vector4
	{
		public float x;
		public float y;
		public float z;
		public float w;
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
	}

	public struct Vector3
	{
		public float x;
		public float y;
		public float z;
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}

	public struct Vector2
	{
		public float x;
		public float y;
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
