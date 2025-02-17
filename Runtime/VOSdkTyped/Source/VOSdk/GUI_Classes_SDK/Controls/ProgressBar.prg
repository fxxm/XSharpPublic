/// <include file="Gui.xml" path="doc/ProgressBar/*" />
CLASS ProgressBar INHERIT Control
	PROTECT dwOldPosition	AS LONG

    PROPERTY ControlType AS ControlType GET ControlType.ProgressBar


	ACCESS __ProgressBar AS IVOProgressBar
		RETURN (IVOProgressBar) oCtrl

	ASSIGN __Value(nValue AS USUAL)  STRICT
		IF IsString(nValue)
			nValue := Val(nValue)
		ENDIF

		SELF:Position := LONGINT(Round(nValue, 0))
		RETURN

/// <include file="Gui.xml" path="doc/ProgressBar.Advance/*" />
	METHOD Advance(dwNewPosition)

		// Instruct the ProgressBar to update its position and save the old position
		IF SELF:ValidateControl()
			dwOldPosition := __ProgressBar:Value
			IF !IsNumeric(dwNewPosition)
				__ProgressBar:PerformStep()
			ELSE
				__ProgressBar:Value := dwNewPosition
			ENDIF
			// Read the position from the control
			RETURN __ProgressBar:Value
		ENDIF
		RETURN 0
	ASSIGN BackgroundColor(oColor AS Color)
		IF SELF:ValidateControl()
			__ProgressBar:BackColor := oColor
		ENDIF
		RETURN

	ASSIGN BarColor(oColor AS Color)
		IF SELF:ValidateControl()
			__ProgressBar:ForeColor := oColor
		ENDIF

		RETURN

/// <include file="Gui.xml" path="doc/ProgressBar.ctor/*" />
	CONSTRUCTOR(oOwner, xID, oPoint, oDimension, kStyle, lDataAware)

		DEFAULT(@lDataAware, TRUE)
		IF IsInstanceOfUsual(xID, #ResourceID)
			SUPER(oOwner, xID, oPoint, oDimension, , kStyle, lDataAware)
		ELSE
			SUPER(oOwner, xID, oPoint, oDimension, "msctls_progress32", kStyle, lDataAware)
		ENDIF
		// Set the default single-step size to 10 range to (0, 100).
		dwOldPosition		:= 0

		RETURN

/// <include file="Gui.xml" path="doc/ProgressBar.OldPosition/*" />
	ACCESS OldPosition AS LONG
		RETURN dwOldPosition

/// <include file="Gui.xml" path="doc/ProgressBar.Position/*" />
	ACCESS Position AS LONG
		RETURN __ProgressBar:Value

/// <include file="Gui.xml" path="doc/ProgressBar.Position/*" />
	ASSIGN Position(dwNewPosition  AS LONG)
		IF SELF:ValidateControl()
			IF dwNewPosition >= __ProgressBar:Minimum .and. dwNewPosition <= __ProgressBar:Maximum
				__ProgressBar:Value	 := dwNewPosition
			ENDIF
		ENDIF




/// <include file="Gui.xml" path="doc/ProgressBar.Range/*" />
	ACCESS Range  AS Range
		IF SELF:ValidateControl()
			RETURN Range{__ProgressBar:Minimum,__ProgressBar:Maximum}
		ENDIF
		RETURN Range{}


/// <include file="Gui.xml" path="doc/ProgressBar.Range/*" />


	ASSIGN Range(oRange  AS Range)
		IF SELF:ValidateControl()
			__ProgressBar:Minimum := oRange:Min
			__ProgressBar:Maximum := oRange:Max
		ENDIF

/// <include file="Gui.xml" path="doc/ProgressBar.UnitSize/*" />
	ACCESS UnitSize  AS INT
		IF SELF:ValidateControl()
			RETURN __ProgressBar:@@Step
		ENDIF
		RETURN 0

/// <include file="Gui.xml" path="doc/ProgressBar.UnitSize/*" />
	ASSIGN UnitSize(dwNewUnitSize AS INT)
		IF SELF:ValidateControl()
			__ProgressBar:@@Step := dwNewUnitSize
		ENDIF
END CLASS

