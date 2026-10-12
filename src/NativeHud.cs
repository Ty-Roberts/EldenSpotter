using System;using System.Drawing;using System.Runtime.InteropServices;
namespace LordranEnemy {public static class NativeHud {
 [StructLayout(LayoutKind.Sequential)]struct Point{public int X,Y;public Point(int x,int y){X=x;Y=y;}}
 [StructLayout(LayoutKind.Sequential)]struct Size{public int X,Y;public Size(int x,int y){X=x;Y=y;}}
 [StructLayout(LayoutKind.Sequential,Pack=1)]struct Blend{public byte Operation,Flags,Alpha,Format;}
 [DllImport("user32.dll")]static extern IntPtr GetDC(IntPtr hwnd);
 [DllImport("user32.dll")]static extern int ReleaseDC(IntPtr hwnd,IntPtr dc);
 [DllImport("gdi32.dll")]static extern IntPtr CreateCompatibleDC(IntPtr dc);
 [DllImport("gdi32.dll")]static extern bool DeleteDC(IntPtr dc);
 [DllImport("gdi32.dll")]static extern IntPtr SelectObject(IntPtr dc,IntPtr obj);
 [DllImport("gdi32.dll")]static extern bool DeleteObject(IntPtr obj);
 [DllImport("user32.dll",SetLastError=true)]static extern bool UpdateLayeredWindow(IntPtr hwnd,IntPtr dc,ref Point pos,ref Size size,IntPtr source,ref Point origin,int key,ref Blend blend,int flags);
 [DllImport("user32.dll")]static extern int GetWindowLong(IntPtr hwnd,int index);
 [DllImport("user32.dll")]static extern int SetWindowLong(IntPtr hwnd,int index,int value);
 [DllImport("user32.dll")]static extern bool DestroyIcon(IntPtr icon);
 public static Icon TrayIcon(){using(var bitmap=new Bitmap(32,32)){using(var g=Graphics.FromImage(bitmap)){g.Clear(Color.Transparent);g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;using(var brush=new SolidBrush(Color.FromArgb(208,177,105)))g.FillPolygon(brush,new System.Drawing.Point[]{new System.Drawing.Point(16,2),new System.Drawing.Point(21,15),new System.Drawing.Point(26,10),new System.Drawing.Point(28,24),new System.Drawing.Point(23,29),new System.Drawing.Point(8,29),new System.Drawing.Point(4,22),new System.Drawing.Point(10,13),new System.Drawing.Point(11,21)});using(var pen=new Pen(Color.FromArgb(240,227,189),2))g.DrawLine(pen,16,7,14,27);}IntPtr handle=bitmap.GetHicon();try{return (Icon)Icon.FromHandle(handle).Clone();}finally{DestroyIcon(handle);}}}
 public static bool IsInteractive(IntPtr hwnd){return (GetWindowLong(hwnd,-20)&0x20)==0;}
 public static void SetInteractive(IntPtr hwnd,bool interactive){int style=GetWindowLong(hwnd,-20);SetWindowLong(hwnd,-20,interactive?style&~0x20:style|0x20);}
 public static void Present(IntPtr hwnd,Bitmap bitmap,System.Drawing.Point location,byte alpha){IntPtr dc=GetDC(IntPtr.Zero),memory=IntPtr.Zero,bmp=IntPtr.Zero,old=IntPtr.Zero;try{memory=CreateCompatibleDC(dc);bmp=bitmap.GetHbitmap(Color.FromArgb(0));old=SelectObject(memory,bmp);var pos=new Point(location.X,location.Y);var size=new Size(bitmap.Width,bitmap.Height);var origin=new Point(0,0);var blend=new Blend{Operation=0,Alpha=alpha,Format=1};if(!UpdateLayeredWindow(hwnd,dc,ref pos,ref size,memory,ref origin,0,ref blend,2))throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());}finally{if(old!=IntPtr.Zero)SelectObject(memory,old);if(bmp!=IntPtr.Zero)DeleteObject(bmp);if(memory!=IntPtr.Zero)DeleteDC(memory);if(dc!=IntPtr.Zero)ReleaseDC(IntPtr.Zero,dc);}}
}}
