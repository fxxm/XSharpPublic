/// <include file="Gui.xml" path="doc/IPAddress/*" />

[XSharp.Internal.TypesChanged];
CLASS IPAddress INHERIT TextControl
	/// <inheritdoc />
    PROPERTY ControlType AS ControlType  GET ControlType.IPAddress

/// <include file="Gui.xml" path="doc/IPAddress.ctor/*" />

	CONSTRUCTOR(oOwner, xID, oPoint, oDimension, dwStyle, lDataAware)
		DEFAULT(@lDataAware, TRUE)

		SUPER(oOwner, xID, oPoint, oDimension, "SysIPAddress32", dwStyle, lDataAware)
		RETURN

 /// <exclude />
	METHOD __SetText(sNewText AS STRING) AS STRING STRICT
		SELF:Address := Val(sNewText)
		RETURN sNewText

 /// <exclude />
	ASSIGN __Value(uValue AS USUAL)  STRICT
		SUPER:__Value := uValue

		IF IsNumeric(uValue)
			SELF:Address := uValue
		ENDIF
		RETURN

/// <include file="Gui.xml" path="doc/IPAddress.Address/*" />
	ACCESS Address AS DWORD
		LOCAL dwAddr AS DWORD
		GuiWin32.SendMessage(oCtrl:Handle, IPM_GETADDRESS, 0, LONGINT(_CAST, @dwAddr))
		RETURN dwAddr


/// <include file="Gui.xml" path="doc/IPAddress.Address/*" />
	ASSIGN Address(nNewAddress AS DWORD)
		LOCAL dwAddr AS DWORD
		dwAddr := nNewAddress
		GuiWin32.SendMessage(oCtrl:Handle, IPM_SETADDRESS, 0, LONGINT(_CAST, dwAddr))

		RETURN

/// <include file="Gui.xml" path="doc/IPAddress.EditHandle/*" />
	ACCESS EditHandle AS IntPtr
		IF SELF:ValidateControl()
			//RETURN GuiWin32.GetWindow(SELF:oCtrl:Handle,GW_CHILD)
			RETURN SELF:oCtrl:Handle
		ENDIF

		RETURN IntPtr.Zero

/// <include file="Gui.xml" path="doc/IPAddress.Field1/*" />
	ACCESS Field1  AS BYTE
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		RETURN (BYTE) _AND(dwAddr >> 24, 0X000000FF)


/// <include file="Gui.xml" path="doc/IPAddress.Field1/*" />
	ASSIGN Field1(iNewVal AS BYTE)
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		dwAddr := _Or(_And(0X00FFFFFFU, dwAddr), (DWORD(_CAST, iNewVal) << 24))
		SELF:Address := dwAddr
		RETURN

/// <include file="Gui.xml" path="doc/IPAddress.Field2/*" />
	ACCESS Field2  AS BYTE
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		RETURN (BYTE) _And((dwAddr >> 16), 0X000000FF)


/// <include file="Gui.xml" path="doc/IPAddress.Field2/*" />
	ASSIGN Field2(iNewVal AS BYTE)
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		dwAddr := _Or(_And(0XFF00FFFF, dwAddr), (DWORD(_CAST, iNewVal) << 16))
		SELF:Address := dwAddr

		RETURN

/// <include file="Gui.xml" path="doc/IPAddress.Field3/*" />
	ACCESS Field3  AS BYTE
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		RETURN (BYTE) _And((dwAddr >> 8), 0X000000FF)


/// <include file="Gui.xml" path="doc/IPAddress.Field3/*" />
	ASSIGN Field3(iNewVal AS BYTE)
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		dwAddr := _Or(_And(0XFFFF00FF, dwAddr), (DWORD(_CAST, iNewVal) << 8))
		SELF:Address := dwAddr
		RETURN

/// <include file="Gui.xml" path="doc/IPAddress.Field4/*" />
	ACCESS Field4  AS BYTE
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		RETURN (BYTE) _And(dwAddr, 0X000000FF)

/// <include file="Gui.xml" path="doc/IPAddress.Field4/*" />
	ASSIGN Field4(iNewVal AS BYTE)
		LOCAL dwAddr AS DWORD
		dwAddr := SELF:Address
		dwAddr := _Or(_And(0XFFFFFF00, dwAddr), (DWORD(_CAST, iNewVal)))
		SELF:Address := dwAddr
		RETURN

/// <include file="Gui.xml" path="doc/IPAddress.SetRange/*" />
    METHOD SetRange(iFieldIndex, iLower, iUpper)
		LOCAL wRange AS LONG
		LOCAL iField AS DWORD

		iField := iFieldIndex -1
		wRange := _Or((LoWord(iUpper) << 8), LoWord( iLower))
		RETURN (GuiWin32.SendMessage(oCtrl:Handle, IPM_SETRANGE, iField, LONGINT(_CAST, wRange)) != 0)


/// <include file="Gui.xml" path="doc/IPAddress.TextValue/*" />
	ACCESS TextValue AS STRING
		RETURN NTrim(SELF:Field1)+"."+NTrim(SELF:Field2)+"."+NTrim(SELF:Field3)+"."+NTrim(SELF:Field4)

/// <include file="Gui.xml" path="doc/IPAddress.TextValue/*" />
	ASSIGN TextValue(cIPAddress  AS STRING)
		//PP-030910
		LOCAL aString AS STRING[]
		LOCAL aField AS DWORD[]
		LOCAL nAddress AS DWORD

		aString := cIPAddress:Split(<Char>{'.'})
		IF aString:Length == 4
			aField := DWORD[]{4}
			aField[1] := Val(aString[1])
			aField[2] := Val(aString[2])
			aField[3] := Val(aString[3])
			aField[4] := Val(aString[4])
			nAddress := aField[1] << 24
			nAddress += aField[2] << 16
			nAddress += aField[3] << 8
			nAddress += aField[4]
			SELF:Address :=  nAddress
		ENDIF

/// <include file="Gui.xml" path="doc/IPAddress.Value/*" />
	ACCESS Value
		RETURN SELF:Address
END CLASS


