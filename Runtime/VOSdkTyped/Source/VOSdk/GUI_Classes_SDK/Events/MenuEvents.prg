// MenuEvents.prg



#include "VOWin32APILibrary.vh"
#USING System.Diagnostics

PARTIAL CLASS MenuEvent INHERIT @@Event
	PROTECT oMenu AS Menu
	PROTECT nID AS LONG

	METHOD AsString() AS STRING STRICT 
		RETURN SELF:HyperLabel:Caption

	ACCESS HyperLabel AS HyperLabel STRICT 
		LOCAL oHyperLabel AS HyperLabel
		IF oMenu != NULL_OBJECT		
			oHyperLabel := oMenu:HyperLabel(nID)
			IF oHyperLabel == NULL
				oHyperLabel := HyperLabel{#Item}
			ENDIF
		ENDIF
		RETURN oHyperLabel

	[DebuggerStepThrough];
	CONSTRUCTOR(uMenu AS USUAL, uWin AS USUAL, uID AS USUAL) STRICT
		SUPER()
		IF IsInstanceOf(uMenu, #Menu)
			oMenu		:= uMenu
		ENDIF
		IF IsInstanceOf(uWin, #Window)
			oWindow		:= uWin
		ENDIF
		IF IsLong(uID)
			SELF:nID := uID
		ENDIF
		RETURN 

	ACCESS ItemID AS LONGINT STRICT 
		RETURN nID

	ACCESS Menu AS Menu 
		RETURN oMenu

	ACCESS Name AS STRING STRICT 
		LOCAL retVal AS STRING
		IF SELF:HyperLabel != NULL_OBJECT
			retVal := SELF:HyperLabel:Name
		ENDIF
		RETURN retVal

	ACCESS NameSym AS SYMBOL STRICT 
		LOCAL retVal AS SYMBOL
		IF SELF:HyperLabel != NULL_OBJECT
			retVal := SELF:HyperLabel:NameSym
		ENDIF
		RETURN retVal

END CLASS


PARTIAL CLASS MenuCommandEvent INHERIT MenuEvent
	[DebuggerStepThrough];
	CONSTRUCTOR(uMenu, uWindow, uItem) 
		SUPER(uMenu, uWindow, uItem) 

END CLASS
PARTIAL CLASS MenuSelectEvent INHERIT MenuEvent
	[DebuggerStepThrough];
	CONSTRUCTOR(uMenu AS USUAL, uWin AS USUAL, uID AS USUAL) STRICT
		SUPER(uMenu, uWin, uID) 

END CLASS

PARTIAL CLASS MenuInitEvent INHERIT MenuEvent
	[DebuggerStepThrough];
	CONSTRUCTOR(uMenu AS USUAL, uWin AS USUAL, uID AS USUAL) STRICT
		SUPER(uMenu, uWin, uID) 

END CLASS

