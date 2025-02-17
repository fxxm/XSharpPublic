/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation.
 *
 * This source code is subject to terms and conditions of the Apache License, Version 2.0. A
 * copy of the license can be found in the License.txt file at the root of this distribution. 
 * 
 * You must not remove this notice, or any other, from this software.
 *
 * ***************************************************************************/
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using XSharpModel;

namespace Microsoft.VisualStudio.Project
{
    public static class NativeMethods
    {
        public static IntPtr InvalidIntPtr = ((IntPtr)((int)(-1)));
        // IIDS

        public static readonly Guid IID_IServiceProvider = typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider).GUID;
        public static readonly Guid IID_IObjectWithSite = typeof(IObjectWithSite).GUID;
        public static readonly Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");

        public static readonly Guid GUID_PropertyBrowserToolWindow = new Guid(unchecked((int)0xeefa5220), unchecked((short)0xe298), (short)0x11d0, new byte[]{ 0x8f, 0x78, 0x0, 0xa0, 0xc9, 0x11, 0x0, 0x57 });

        [ComImport,System.Runtime.InteropServices.Guid("5EFC7974-14BC-11CF-9B2B-00AA00573819")]
        public class OleComponentUIManager {
        }

        // packing
        public static int SignedHIWORD(int n) {
            return (int)(short)((n >> 16) & 0xffff);
        }

        public static int SignedLOWORD(int n) {
            return (int)(short)(n & 0xFFFF);
        }

        public const int
        CLSCTX_INPROC_SERVER = 0x1;

        public const int
        S_FALSE = 0x00000001,
        S_OK = 0x00000000,

        IDOK = 1,
        IDCANCEL = 2,
        IDABORT = 3,
        IDRETRY = 4,
        IDIGNORE = 5,
        IDYES = 6,
        IDNO = 7,
        IDCLOSE = 8,
        IDHELP = 9,
        IDTRYAGAIN = 10,
        IDCONTINUE = 11,

        OLECMDERR_E_NOTSUPPORTED = unchecked((int)0x80040100),
        OLECMDERR_E_UNKNOWNGROUP = unchecked((int)0x80040104),

        UNDO_E_CLIENTABORT = unchecked((int)0x80044001),
        E_OUTOFMEMORY = unchecked((int)0x8007000E),
        E_INVALIDARG = unchecked((int)0x80070057),
        E_FAIL = unchecked((int)0x80004005),
        E_NOINTERFACE = unchecked((int)0x80004002),
        E_POINTER = unchecked((int)0x80004003),
        E_NOTIMPL = unchecked((int)0x80004001),
        E_UNEXPECTED = unchecked((int)0x8000FFFF),
        E_HANDLE = unchecked((int)0x80070006),
        E_ABORT = unchecked((int)0x80004004),
        E_ACCESSDENIED = unchecked((int)0x80070005),
        E_PENDING = unchecked((int)0x8000000A);

        public const int
        OLECLOSE_SAVEIFDIRTY = 0,
        OLECLOSE_NOSAVE = 1,
        OLECLOSE_PROMPTSAVE = 2;

        public const int
        OLEIVERB_PRIMARY = 0,
        OLEIVERB_SHOW = -1,
        OLEIVERB_OPEN = -2,
        OLEIVERB_HIDE = -3,
        OLEIVERB_UIACTIVATE = -4,
        OLEIVERB_INPLACEACTIVATE = -5,
        OLEIVERB_DISCARDUNDOSTATE = -6,
        OLEIVERB_PROPERTIES = -7;

        public const int
        OFN_READONLY = unchecked((int)0x00000001),
        OFN_OVERWRITEPROMPT = unchecked((int)0x00000002),
        OFN_HIDEREADONLY = unchecked((int)0x00000004),
        OFN_NOCHANGEDIR = unchecked((int)0x00000008),
        OFN_SHOWHELP = unchecked((int)0x00000010),
        OFN_ENABLEHOOK = unchecked((int)0x00000020),
        OFN_ENABLETEMPLATE = unchecked((int)0x00000040),
        OFN_ENABLETEMPLATEHANDLE = unchecked((int)0x00000080),
        OFN_NOVALIDATE = unchecked((int)0x00000100),
        OFN_ALLOWMULTISELECT = unchecked((int)0x00000200),
        OFN_EXTENSIONDIFFERENT = unchecked((int)0x00000400),
        OFN_PATHMUSTEXIST = unchecked((int)0x00000800),
        OFN_FILEMUSTEXIST = unchecked((int)0x00001000),
        OFN_CREATEPROMPT = unchecked((int)0x00002000),
        OFN_SHAREAWARE = unchecked((int)0x00004000),
        OFN_NOREADONLYRETURN = unchecked((int)0x00008000),
        OFN_NOTESTFILECREATE = unchecked((int)0x00010000),
        OFN_NONETWORKBUTTON = unchecked((int)0x00020000),
        OFN_NOLONGNAMES = unchecked((int)0x00040000),
        OFN_EXPLORER = unchecked((int)0x00080000),
        OFN_NODEREFERENCELINKS = unchecked((int)0x00100000),
        OFN_LONGNAMES = unchecked((int)0x00200000),
        OFN_ENABLEINCLUDENOTIFY = unchecked((int)0x00400000),
        OFN_ENABLESIZING = unchecked((int)0x00800000),
        OFN_USESHELLITEM = unchecked((int)0x01000000),
        OFN_DONTADDTORECENT = unchecked((int)0x02000000),
        OFN_FORCESHOWHIDDEN = unchecked((int)0x10000000);

        // for READONLYSTATUS
        public const int
        ROSTATUS_NotReadOnly = 0x0,
        ROSTATUS_ReadOnly = 0x1,
        ROSTATUS_Unknown = unchecked((int)0xFFFFFFFF);

        public const int
        IEI_DoNotLoadDocData = 0x10000000;

        public static readonly Guid LOGVIEWID_Any = new Guid(0xffffffff, 0xffff, 0xffff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff);

        public const int
        CB_SETDROPPEDWIDTH = 0x0160,

        GWL_STYLE = (-16),
        GWL_EXSTYLE = (-20),

        DWL_MSGRESULT = 0,

        SW_SHOWNORMAL = 1,

        HTMENU = 5,

        WS_POPUP = unchecked((int)0x80000000),
        WS_CHILD = 0x40000000,
        WS_MINIMIZE = 0x20000000,
        WS_VISIBLE = 0x10000000,
        WS_DISABLED = 0x08000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_MAXIMIZE = 0x01000000,
        WS_CAPTION = 0x00C00000,
        WS_BORDER = 0x00800000,
        WS_DLGFRAME = 0x00400000,
        WS_VSCROLL = 0x00200000,
        WS_HSCROLL = 0x00100000,
        WS_SYSMENU = 0x00080000,
        WS_THICKFRAME = 0x00040000,
        WS_TABSTOP = 0x00010000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_EX_DLGMODALFRAME = 0x00000001,
        WS_EX_MDICHILD = 0x00000040,
        WS_EX_TOOLWINDOW = 0x00000080,
        WS_EX_CLIENTEDGE = 0x00000200,
        WS_EX_CONTEXTHELP = 0x00000400,
        WS_EX_RIGHT = 0x00001000,
        WS_EX_LEFT = 0x00000000,
        WS_EX_RTLREADING = 0x00002000,
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        WS_EX_CONTROLPARENT = 0x00010000,
        WS_EX_STATICEDGE = 0x00020000,
        WS_EX_APPWINDOW = 0x00040000,
        WS_EX_LAYERED = 0x00080000,
        WS_EX_TOPMOST = 0x00000008,
        WS_EX_NOPARENTNOTIFY = 0x00000004,

        LVM_SETEXTENDEDLISTVIEWSTYLE = (0x1000 + 54),

        LVS_EX_LABELTIP = 0x00004000,

        // winuser.h
            WH_JOURNALPLAYBACK = 1,
        WH_GETMESSAGE = 3,
        WH_MOUSE = 7,
        WSF_VISIBLE = 0x0001,
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DELETEITEM = 0x002D,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WA_INACTIVE = 0,
        WA_ACTIVE = 1,
        WA_CLICKACTIVE = 2,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_QUERYOPEN = 0x0013,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_WININICHANGE = 0x001A,
        WM_SETTINGCHANGE = 0x001A,
        WM_DEVMODECHANGE = 0x001B,
        WM_ACTIVATEAPP = 0x001C,
        WM_FONTCHANGE = 0x001D,
        WM_TIMECHANGE = 0x001E,
        WM_CANCELMODE = 0x001F,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002A,
        WM_DRAWITEM = 0x002B,
        WM_MEASUREITEM = 0x002C,
        WM_VKEYTOITEM = 0x002E,
        WM_CHARTOITEM = 0x002F,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003D,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYDATA = 0x004A,
        WM_CANCELJOURNAL = 0x004B,
        WM_NOTIFY = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_NCXBUTTONDBLCLK = 0x00AD,
        WM_KEYFIRST = 0x0100,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_CTLCOLOR = 0x0019,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        WM_KEYLAST = 0x0108,
        WM_IME_STARTCOMPOSITION = 0x010D,
        WM_IME_ENDCOMPOSITION = 0x010E,
        WM_IME_COMPOSITION = 0x010F,
        WM_IME_KEYLAST = 0x010F,
        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSELAST = 0x020A,
        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,
        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,
        WM_POWERBROADCAST = 0x0218,
        WM_DEVICECHANGE = 0x0219,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_KEYDOWN = 0x0290,
        WM_IME_KEYUP = 0x0291,
        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,
        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,
        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030A,
        WM_SIZECLIPBOARD = 0x030B,
        WM_ASKCBFORMATNAME = 0x030C,
        WM_CHANGECBCHAIN = 0x030D,
        WM_HSCROLLCLIPBOARD = 0x030E,
        WM_QUERYNEWPALETTE = 0x030F,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,
        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035F,
        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037F,
        WM_PENWINFIRST = 0x0380,
        WM_PENWINLAST = 0x038F,
        WM_APP = unchecked((int)0x8000),
        WM_USER = 0x0400,
        WM_REFLECT =
        WM_USER + 0x1C00,
        WS_OVERLAPPED = 0x00000000,
        WPF_SETMINPOSITION = 0x0001,
        WM_CHOOSEFONT_GETLOGFONT = (0x0400 + 1),

        WHEEL_DELTA = 120,
        DWLP_MSGRESULT = 0,
        PSNRET_NOERROR = 0,
        PSNRET_INVALID = 1,
        PSNRET_INVALID_NOCHANGEPAGE = 2;

        public const int
        PSN_APPLY = ((0 - 200) - 2),
        PSN_KILLACTIVE = ((0 - 200) - 1),
        PSN_RESET = ((0 - 200) - 3),
        PSN_SETACTIVE = ((0 - 200) - 0);

        public const int
        GMEM_MOVEABLE = 0x0002,
        GMEM_ZEROINIT = 0x0040,
        GMEM_DDESHARE = 0x2000;

        public const int
        SWP_NOACTIVATE = 0x0010,
        SWP_NOZORDER = 0x0004,
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_FRAMECHANGED = 0x0020;

        public const int
        TVM_SETINSERTMARK = (0x1100 + 26),
        TVM_GETEDITCONTROL = (0x1100 + 15);

        public const int
        FILE_ATTRIBUTE_READONLY = 0x00000001;

        public const int
            PSP_DEFAULT = 0x00000000,
            PSP_DLGINDIRECT = 0x00000001,
            PSP_USEHICON = 0x00000002,
            PSP_USEICONID = 0x00000004,
            PSP_USETITLE = 0x00000008,
            PSP_RTLREADING = 0x00000010,
            PSP_HASHELP = 0x00000020,
            PSP_USEREFPARENT = 0x00000040,
            PSP_USECALLBACK = 0x00000080,
            PSP_PREMATURE = 0x00000400,
            PSP_HIDEHEADER = 0x00000800,
            PSP_USEHEADERTITLE = 0x00001000,
            PSP_USEHEADERSUBTITLE = 0x00002000;

        public const int
            PSH_DEFAULT = 0x00000000,
            PSH_PROPTITLE = 0x00000001,
            PSH_USEHICON = 0x00000002,
            PSH_USEICONID = 0x00000004,
            PSH_PROPSHEETPAGE = 0x00000008,
            PSH_WIZARDHASFINISH = 0x00000010,
            PSH_WIZARD = 0x00000020,
            PSH_USEPSTARTPAGE = 0x00000040,
            PSH_NOAPPLYNOW = 0x00000080,
            PSH_USECALLBACK = 0x00000100,
            PSH_HASHELP = 0x00000200,
            PSH_MODELESS = 0x00000400,
            PSH_RTLREADING = 0x00000800,
            PSH_WIZARDCONTEXTHELP = 0x00001000,
            PSH_WATERMARK = 0x00008000,
            PSH_USEHBMWATERMARK = 0x00010000,  // user pass in a hbmWatermark instead of pszbmWatermark
            PSH_USEHPLWATERMARK = 0x00020000,  //
            PSH_STRETCHWATERMARK = 0x00040000,  // stretchwatermark also applies for the header
            PSH_HEADER = 0x00080000,
            PSH_USEHBMHEADER = 0x00100000,
            PSH_USEPAGELANG = 0x00200000,  // use frame dialog template matched to page
            PSH_WIZARD_LITE = 0x00400000,
            PSH_NOCONTEXTHELP = 0x02000000;

        public const int
            PSBTN_BACK = 0,
            PSBTN_NEXT = 1,
            PSBTN_FINISH = 2,
            PSBTN_OK = 3,
            PSBTN_APPLYNOW = 4,
            PSBTN_CANCEL = 5,
            PSBTN_HELP = 6,
            PSBTN_MAX = 6;

        public const int
            TRANSPARENT = 1,
            OPAQUE = 2,
            FW_BOLD = 700;

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        public class LOGFONT {
            public int lfHeight = 0;
            public int lfWidth = 0;
            public int lfEscapement = 0;
            public int lfOrientation = 0;
            public int lfWeight = 0;
            public byte lfItalic = 0;
            public byte lfUnderline = 0;
            public byte lfStrikeOut = 0;
            public byte lfCharSet = 0;
            public byte lfOutPrecision = 0;
            public byte lfClipPrecision = 0;
            public byte lfQuality = 0;
            public byte lfPitchAndFamily = 0;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
            public string   lfFaceName = null;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public int idFrom;
            public int code;
        }

        /// <devdoc>
        /// Helper class for setting the text parameters to OLECMDTEXT structures.
        /// </devdoc>
        public static class OLECMDTEXT {
            public static void SetText(IntPtr pCmdTextInt, string text) {
                Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT pCmdText = (Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT)Marshal.PtrToStructure(pCmdTextInt, typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT));
                char[] menuText = text.ToCharArray();

                // Get the offset to the rgsz param.  This is where we will stuff our text
                //
                IntPtr offset = Marshal.OffsetOf(typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT), "rgwz");
                IntPtr offsetToCwActual = Marshal.OffsetOf(typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT), "cwActual");

                // The max chars we copy is our string, or one less than the buffer size,
                // since we need a null at the end.
                //
                int maxChars = Math.Min((int)pCmdText.cwBuf - 1, menuText.Length);

                Marshal.Copy(menuText, 0, (IntPtr)((long)pCmdTextInt + (long)offset), maxChars);

                // append a null character
                Marshal.WriteInt16((IntPtr)((long)pCmdTextInt + (long)offset + maxChars * 2), 0);

                // write out the length
                // +1 for the null char
                Marshal.WriteInt32((IntPtr)((long)pCmdTextInt + (long)offsetToCwActual), maxChars + 1);
            }

            /// <summary>
            /// Gets the flags of the OLECMDTEXT structure
            /// </summary>
            /// <param name="pCmdTextInt">The structure to read.</param>
            /// <returns>The value of the flags.</returns>
            public static OLECMDTEXTF GetFlags(IntPtr pCmdTextInt) {
                Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT pCmdText = (Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT)Marshal.PtrToStructure(pCmdTextInt, typeof(Microsoft.VisualStudio.OLE.Interop.OLECMDTEXT));

                if ((pCmdText.cmdtextf & (int)OLECMDTEXTF.OLECMDTEXTF_NAME) != 0)
                    return OLECMDTEXTF.OLECMDTEXTF_NAME;

                if ((pCmdText.cmdtextf & (int)OLECMDTEXTF.OLECMDTEXTF_STATUS) != 0)
                    return OLECMDTEXTF.OLECMDTEXTF_STATUS;

                return OLECMDTEXTF.OLECMDTEXTF_NONE;
            }


            /// <summary>
            /// Flags for the OLE command text
            /// </summary>
            public enum OLECMDTEXTF
            {
                /// <summary>No flag</summary>
                OLECMDTEXTF_NONE = 0,
                /// <summary>The name of the command is required.</summary>
                OLECMDTEXTF_NAME = 1,
                /// <summary>A description of the status is required.</summary>
                OLECMDTEXTF_STATUS = 2
            }
        }

        /// <devdoc>
        /// OLECMDF enums for IOleCommandTarget
        /// </devdoc>
        public enum tagOLECMDF
        {
            OLECMDF_SUPPORTED = 1,
            OLECMDF_ENABLED = 2,
            OLECMDF_LATCHED = 4,
            OLECMDF_NINCHED = 8,
            OLECMDF_INVISIBLE = 16
        }

        /// <devdoc>
        /// Constants for stream usage.
        /// </devdoc>
        public sealed class StreamConsts {
            public const   int LOCK_WRITE = 0x1;
            public const   int LOCK_EXCLUSIVE = 0x2;
            public const   int LOCK_ONLYONCE = 0x4;
            public const   int STATFLAG_DEFAULT = 0x0;
            public const   int STATFLAG_NONAME = 0x1;
            public const   int STATFLAG_NOOPEN = 0x2;
            public const   int STGC_DEFAULT = 0x0;
            public const   int STGC_OVERWRITE = 0x1;
            public const   int STGC_ONLYIFCURRENT = 0x2;
            public const   int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 0x4;
            public const   int STREAM_SEEK_SET = 0x0;
            public const   int STREAM_SEEK_CUR = 0x1;
            public const   int STREAM_SEEK_END = 0x2;
        }

        /// <devdoc>
        /// This class implements a managed Stream object on top
        /// of a COM IStream
        /// </devdoc>
        internal sealed class DataStreamFromComStream : Stream, IDisposable {

            private Microsoft.VisualStudio.OLE.Interop.IStream comStream;

            #if DEBUG
            private string creatingStack;
            #endif

            public DataStreamFromComStream(Microsoft.VisualStudio.OLE.Interop.IStream comStream) : base() {
                this.comStream = comStream;

                #if DEBUG
                creatingStack = Environment.StackTrace;
                #endif
            }

            public override long Position {
                get {
                    ThreadHelper.ThrowIfNotOnUIThread();
                    return Seek(0, SeekOrigin.Current);
                }

                set {
                    ThreadHelper.ThrowIfNotOnUIThread();
                    Seek(value, SeekOrigin.Begin);
                }
            }

            public override bool CanWrite {
                get {
                    return true;
                }
            }

            public override bool CanSeek {
                get {
                    return true;
                }
            }

            public override bool CanRead {
                get {
                    return true;
                }
            }

            public override long Length {
                get {
                    ThreadHelper.ThrowIfNotOnUIThread();
                    long curPos = this.Position;
                    long endPos = Seek(0, SeekOrigin.End);
                    this.Position = curPos;
                    return endPos - curPos;
                }
            }

            private void _NotImpl(string message) {
                NotSupportedException ex = new NotSupportedException(message, new ExternalException(String.Empty, VSConstants.E_NOTIMPL));
                throw ex;
            }

            protected override void Dispose(bool disposing) {
                try {
                    ThreadHelper.ThrowIfNotOnUIThread();

                    if (disposing) {
                        if (comStream != null) {
                            Flush();
                        }
                    }
                    // Cannot close COM stream from finalizer thread.
                    comStream = null;
                }
                finally {
                    base.Dispose(disposing);
                }
            }

            public override void Flush() {
                ThreadHelper.ThrowIfNotOnUIThread();

                if (comStream != null) {
                    try {
                        comStream.Commit(StreamConsts.STGC_DEFAULT);
                    }
                    catch {
                    }
                }
            }

            public override int Read(byte[] buffer, int index, int count) {
                uint bytesRead;
                byte[] b = buffer;

                if (index != 0) {
                    b = new byte[buffer.Length - index];
                    buffer.CopyTo(b, 0);
                }
                ThreadHelper.ThrowIfNotOnUIThread();

                comStream.Read(b, (uint)count, out bytesRead);

                if (index != 0) {
                    b.CopyTo(buffer, index);
                }

                return (int)bytesRead;
            }

            public override void SetLength(long value) {
                ThreadHelper.ThrowIfNotOnUIThread();
                ULARGE_INTEGER ul = new ULARGE_INTEGER();
                ul.QuadPart = (ulong)value;
                comStream.SetSize(ul);
            }

            public override long Seek(long offset, SeekOrigin origin) {
                ThreadHelper.ThrowIfNotOnUIThread();
                LARGE_INTEGER l = new LARGE_INTEGER();
                ULARGE_INTEGER[] ul = new ULARGE_INTEGER[1];
                ul[0] = new ULARGE_INTEGER();
                l.QuadPart = offset;
                comStream.Seek(l, (uint)origin, ul);
                return (long)ul[0].QuadPart;
            }

            public override void Write(byte[] buffer, int index, int count) {
                uint bytesWritten;
                ThreadHelper.ThrowIfNotOnUIThread();

                if (count > 0) {

                    byte[] b = buffer;

                    if (index != 0) {
                        b = new byte[buffer.Length - index];
                        buffer.CopyTo(b, 0);
                    }

                    comStream.Write(b, (uint)count, out bytesWritten);
                    if (bytesWritten != count)
                        // Didn't write enough bytes to IStream!
                        throw new IOException();

                    if (index != 0) {
                        b.CopyTo(buffer, index);
                    }
                }
            }

            ~DataStreamFromComStream() {
                #if DEBUG
                if (comStream != null) {
                    Debug.Fail("DataStreamFromComStream not closed.  Creating stack: " + creatingStack);
                }
                #endif
                // CANNOT CLOSE NATIVE STREAMS IN FINALIZER THREAD
                // Close();
            }
        }

        /// <devdoc>
        /// Class that encapsulates a connection point cookie for COM event handling.
        /// </devdoc>
        public sealed class ConnectionPointCookie : IDisposable {
            private Microsoft.VisualStudio.OLE.Interop.IConnectionPointContainer cpc;
            private Microsoft.VisualStudio.OLE.Interop.IConnectionPoint connectionPoint;
            private uint cookie;
            #if DEBUG
            private string callStack ="(none)";
            private Type   eventInterface;
            #endif

            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="ConnectionPointCookie.ConnectionPointCookie"]/*' />
            /// <devdoc>
            /// Creates a connection point to of the given interface type.
            /// which will call on a managed code sink that implements that interface.
            /// </devdoc>
            public ConnectionPointCookie(object source, object sink, Type eventInterface) : this(source, sink, eventInterface, true){
                ThreadHelper.ThrowIfNotOnUIThread();
            }
#pragma warning disable VSTHRD010
            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="NativeMethods.Finalize1"]/*' />
            ~ConnectionPointCookie(){
                #if DEBUG
                System.Diagnostics.Debug.Assert(connectionPoint == null || cookie == 0, "We should never finalize an active connection point. (Interface = " + eventInterface.FullName + "), allocating code (see stack) is responsible for unhooking the ConnectionPoint by calling Disconnect.  Hookup Stack =\r\n" +  callStack);
#endif

                // We must pass false here because chances are that if someone
                // forgot to Dispose this object, the IConnectionPoint is likely to be
                // a disconnected RCW at this point (for example, we are far along in a
                // VS shutdown scenario).  The result will be a memory leak, which is the
                // expected result for an undisposed IDisposable object. [clovett] bug 369592.
                Dispose(false);
            }
#pragma warning restore VSTHRD010
            public void Dispose() {
                ThreadHelper.ThrowIfNotOnUIThread();
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing) {
                ThreadHelper.ThrowIfNotOnUIThread();
                if (disposing) {
                    try {
                        if (connectionPoint != null && cookie != 0) {
                            connectionPoint.Unadvise(cookie);
                        }
                    }
                    finally {
                        this.cookie = 0;
                        this.connectionPoint = null;
                        this.cpc = null;
                    }
                }
            }

            /// <devdoc>
            /// Creates a connection point to of the given interface type.
            /// which will call on a managed code sink that implements that interface.
            /// </devdoc>
            public ConnectionPointCookie(object source, object sink, Type eventInterface, bool throwException){
                Exception ex = null;
                ThreadHelper.ThrowIfNotOnUIThread();
                if (source is Microsoft.VisualStudio.OLE.Interop.IConnectionPointContainer) {
                    this.cpc = (Microsoft.VisualStudio.OLE.Interop.IConnectionPointContainer)source;

                    try {
                        Guid tmp = eventInterface.GUID;
                        cpc.FindConnectionPoint(ref tmp, out connectionPoint);
                    }
                    catch {
                        connectionPoint = null;
                    }

                    if (connectionPoint == null) {
                        ex = new ArgumentException(/* SR.GetString(SR.ConnectionPoint_SourceIF, eventInterface.Name)*/);
                    }
                    else if (sink == null || !eventInterface.IsInstanceOfType(sink)) {
                        ex = new InvalidCastException(/* SR.GetString(SR.ConnectionPoint_SinkIF)*/);
                    }
                    else {
                        try {
                            connectionPoint.Advise(sink, out cookie);
                        }
                        catch {
                            cookie = 0;
                            connectionPoint = null;
                            ex = new Exception(/*SR.GetString(SR.ConnectionPoint_AdviseFailed, eventInterface.Name)*/);
                        }
                    }
                }
                else {
                    ex = new InvalidCastException(/*SR.ConnectionPoint_SourceNotICP)*/);
                }


                if (throwException && (connectionPoint == null || cookie == 0)) {
                    if (ex == null) {
                        throw new ArgumentException(/*SR.GetString(SR.ConnectionPoint_CouldNotCreate, eventInterface.Name)*/);
                    }
                    else {
                        throw ex;
                    }
                }

                #if DEBUG
                callStack = Environment.StackTrace;
                this.eventInterface = eventInterface;
                #endif
            }
        }
        /// <devdoc>
        /// This method takes a file URL and converts it to an absolute path.  The trick here is that
        /// if there is a '#' in the path, everything after this is treated as a fragment.  So
        /// we need to append the fragment to the end of the path.
        /// </devdoc>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static string GetAbsolutePath(string fileName) {
            System.Diagnostics.Debug.Assert(fileName != null && fileName.Length > 0, "Cannot get absolute path, fileName is not valid");

            Uri uri = new Uri(fileName);
            return uri.LocalPath + uri.Fragment;
        }

        /// <devdoc>
        /// This method takes a file URL and converts it to a local path.  The trick here is that
        /// if there is a '#' in the path, everything after this is treated as a fragment.  So
        /// we need to append the fragment to the end of the path.
        /// </devdoc>
        public static string GetLocalPath(string fileName) {
            System.Diagnostics.Debug.Assert(fileName != null && fileName.Length > 0, "Cannot get local path, fileName is not valid");

            Uri uri = new Uri(fileName);
            return uri.LocalPath + uri.Fragment;
        }

        /// <devdoc>
        /// Please use this "approved" method to compare file names.
        /// </devdoc>
        public static bool IsSamePath(string file1, string file2)
        {
            if(file1 == null || file1.Length == 0)
            {
                return (file2 == null || file2.Length == 0);
            }

            Uri uri1 = null;
            Uri uri2 = null;

            try
            {
                if(!Uri.TryCreate(file1, UriKind.Absolute, out uri1) || !Uri.TryCreate(file2, UriKind.Absolute, out uri2))
                {
                    return false;
                }

                if(uri1 != null && uri1.IsFile && uri2 != null && uri2.IsFile)
                {
                    return 0 == String.Compare(uri1.LocalPath, uri2.LocalPath, StringComparison.OrdinalIgnoreCase);
                }

                return file1 == file2;
            }
            catch(UriFormatException e)
            {

                XSettings.LogException(e, "IsSamePath");
            }

            return false;
        }

        [ComImport(), Guid("73EB9EF8-6F2C-4493-8D04-40D2289774D4"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEventHandler
        {

            // converts the underlying codefunction into an event handler for the given event
            // if the given event is NULL, then the function will handle no events
            [PreserveSig]
            int AddHandler(string bstrEventName);

            [PreserveSig]
            int RemoveHandler(string bstrEventName);

            IVsEnumBSTR GetHandledEvents();

            bool HandlesEvent(string bstrEventName);
        }

        [ComImport(), Guid("ABCDD339-9474-4972-B1FD-024F90716CD4"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IParameterKind
        {

            void SetParameterPassingMode(PARAMETER_PASSING_MODE ParamPassingMode);
            void SetParameterArrayDimensions(int uDimensions);
            int GetParameterArrayCount();
            int GetParameterArrayDimensions(int uIndex);
            int GetParameterPassingMode();
        }

        public enum PARAMETER_PASSING_MODE
        {
            cmParameterTypeIn = 1,
            cmParameterTypeOut = 2,
            cmParameterTypeInOut = 3
        }

        [
        ComImport, ComVisible(true), Guid("E2D947FB-9ADA-44BB-8F78-E6E778EC4F61"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IMethodXML
        {
            // Generate XML describing the contents of this function's body.
            void GetXML(ref string pbstrXML);

            // Parse the incoming XML with respect to the CodeModel XML schema and
            // use the result to regenerate the body of the function.
            /// <include file='doc\NativeMethods.uex' path='docs/doc[@for="IMethodXML.SetXML"]/*' />
            [PreserveSig]
            int SetXML(string pszXML);

            // This is really a textpoint
            [PreserveSig]
            int GetBodyPoint([MarshalAs(UnmanagedType.Interface)]out object bodyPoint);
        }

        [ComImport(), Guid("E24FF894-FCFB-4E01-B2DE-EC30D616473C"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IVBFileCodeModelEvents
        {

            [PreserveSig]
            int StartEdit();

            [PreserveSig]
            int EndEdit();
        }

        ///--------------------------------------------------------------------------
        /// ICodeClassBase:
        ///--------------------------------------------------------------------------
        [GuidAttribute("23BBD58A-7C59-449b-A93C-43E59EFC080C")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport()]
        public interface ICodeClassBase
        {
            [PreserveSig()]
            int GetBaseName(out string pBaseName);
        }

        public const ushort CF_HDROP = 15; // winuser.h
        public const uint MK_CONTROL = 0x0008; //winuser.h
        public const uint MK_SHIFT = 0x0004;
        public const int MAX_PATH = 260; // windef.h
        public const int MAX_FOLDER_PATH = MAX_PATH - 12;   // folders need to allow 8.3 filenames, so MAX_PATH - 12

        /// <summary>
        /// Specifies options for a bitmap image associated with a task item.
        /// </summary>
        public enum VSTASKBITMAP
        {
            BMP_COMPILE = -1,
            BMP_SQUIGGLE = -2,
            BMP_COMMENT = -3,
            BMP_SHORTCUT = -4,
            BMP_USER = -5
        };

        public const int ILD_NORMAL = 0x0000,
            ILD_TRANSPARENT = 0x0001,
            ILD_MASK = 0x0010,
            ILD_ROP = 0x0040;

        /// <summary>
        /// Defines the values that are not supported by the System.Environment.SpecialFolder enumeration
        /// </summary>
        [ComVisible(true)]
        public enum ExtendedSpecialFolder
        {
            /// <summary>
            /// Identical to CSIDL_COMMON_STARTUP
            /// </summary>
            CommonStartup = 0x0018,

            /// <summary>
            /// Identical to CSIDL_WINDOWS
            /// </summary>
            Windows = 0x0024,
        }


        // APIS

        /// <summary>
        /// Changes the parent window of the specified child window.
        /// </summary>
        /// <param name="hWnd">Handle to the child window.</param>
        /// <param name="hWndParent">Handle to the new parent window. If this parameter is NULL, the desktop window becomes the new parent window.</param>
        /// <returns>A handle to the previous parent window indicates success. NULL indicates failure.</returns>
        [DllImport("User32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "IsDialogMessageA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern bool IsDialogMessageA(IntPtr hDlg, ref MSG msg);

        /// <summary>
        /// Indicates whether the file type is binary or not
        /// </summary>
        /// <param name="lpApplicationName">Full path to the file to check</param>
        /// <param name="lpBinaryType">If file isbianry the bitness of the app is indicated by lpBinaryType value.</param>
        /// <returns>True if the file is binary false otherwise</returns>
        [DllImport("kernel32.dll")]
        public static extern bool GetBinaryType([MarshalAs(UnmanagedType.LPWStr)]string lpApplicationName, out uint lpBinaryType);

    }
}

