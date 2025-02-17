/// <include file="Gui.xml" path="doc/CurHand/*" />

CLASS CurHand INHERIT Pointer

/// <include file="Gui.xml" path="doc/CurHand.ctor/*" />
	CONSTRUCTOR ()
		SUPER(ResourceID{"CURHAND", _GetInst()})
		RETURN

END CLASS

/// <include file="Gui.xml" path="doc/HyperLink/*" />
[XSharp.Internal.TypesChanged];
CLASS HyperLink INHERIT FixedText
	/// <inheritdoc />
    PROPERTY Controltype AS Controltype GET Controltype.Label
    /// <inheritdoc />
    METHOD OnControlCreated(oC AS IVOControl) AS VOID
        VAR oLabel := (IVOLabel) oC
        oLabel:Click += Clicked

    METHOD Clicked(sender AS OBJECT, e AS EventArgs) AS VOID
       SELF:OpenLink()
        RETURN

/// <include file="Gui.xml" path="doc/HyperLink.ctor/*" />
    CONSTRUCTOR(oOwner, xID, oPoint, oDimension, cText)
		SUPER(oOwner, xID, oPoint, oDimension, cText )
        oCtrl:Cursor := System.Windows.Forms.Cursors.Hand
		RETURN

/// <include file="Gui.xml" path="doc/HyperLink.OpenLink/*" />
	METHOD OpenLink()
		ShellOpen(SELF:Owner, SELF:Caption)
		RETURN SELF
END CLASS

/// <exclude/>
FUNCTION ShellOpen(oWindow AS Window, cFile AS STRING) AS VOID STRICT
	LOCAL hWnd AS PTR

	cFile := AllTrim(Lower(cFile))
	IF At2("@",cFile)>1
	    IF ! cFile = "mailto:"
	        cFile := "mailto:" + cFile
	    ENDIF
	ENDIF
	IF ! Empty(cFile)
	    IF oWindow != NULL_OBJECT
	        hWnd := oWindow:Handle()
	    ELSE
	        hWnd := NULL_PTR //Desktop
	    ENDIF
	    ShellExecute(hWnd, "open", cFile, NULL, NULL, SW_SHOWNORMAL)
    ENDIF
	RETURN

_DLL FUNCTION ShellExecute( hWnd AS IntPtr, lpOperation AS STRING, lpFile AS STRING,;
	lpParameters AS STRING, lpDirectory AS STRING, nShowCmd AS INT) AS IntPtr PASCAL:SHELL32.ShellExecuteA ANSI


