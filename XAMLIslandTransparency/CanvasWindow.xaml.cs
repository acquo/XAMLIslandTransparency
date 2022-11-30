using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using Vanara.PInvoke;

namespace XAMLIslandTransparency
{
    public partial class CanvasWindow : Window
    {
        public struct STYLESTRUCT
        {
            public User32.WindowStylesEx styleOld;
            public User32.WindowStylesEx styleNew;
        }

        private IntPtr _hwnd;
        private int CurrentExStyle => (int)User32.GetWindowLongPtr(_hwnd, User32.WindowLongFlags.GWL_EXSTYLE);

        public void SetTransparentHitThrough() =>
            User32.SetWindowLong(_hwnd, User32.WindowLongFlags.GWL_EXSTYLE, 
                CurrentExStyle | (int)User32.WindowStylesEx.WS_EX_TRANSPARENT);

        public void SetTransparentNotHitThrough() =>
            User32.SetWindowLong(_hwnd, User32.WindowLongFlags.GWL_EXSTYLE, 
                CurrentExStyle & ~(int)User32.WindowStylesEx.WS_EX_TRANSPARENT);

        public bool CanClose { get; set; } = true;

        public CanvasWindow()
        {
            InitializeComponent();
            _hwnd = new WindowInteropHelper(this).EnsureHandle();
            Loaded += CanvasWindow_Loaded;
            Closing += CanvasWindow_Closing;
        }

        private void CanvasWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!CanClose)
                e.Cancel = true;
        }

        private void CanvasWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ((HwndSource)PresentationSource.FromVisual(this)).AddHook(
                 (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
                 {
                     if (msg == (int)User32.WindowMessage.WM_STYLECHANGING &&
                         (long)wParam == (long)User32.WindowLongFlags.GWL_EXSTYLE)
                     {
                         var styleStruct = (STYLESTRUCT)Marshal.PtrToStructure(lParam, typeof(STYLESTRUCT));
                         styleStruct.styleNew |= User32.WindowStylesEx.WS_EX_LAYERED;
                         Marshal.StructureToPtr(styleStruct, lParam, false);
                         handled = true;
                     }
                     return IntPtr.Zero;
                 });
        }
    }
}
