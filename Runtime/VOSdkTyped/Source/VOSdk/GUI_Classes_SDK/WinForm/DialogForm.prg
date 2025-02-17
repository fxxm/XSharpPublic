// DialogForm.prg

USING System.Windows.Forms
USING System.ComponentModel
CLASS VODialogForm INHERIT VOForm
	PROTECT	oSurfacePanel	AS VOSurfacePanel
	PROTECT oResDlg			AS ResourceDialog
	PROTECT lMustAdjust		AS LOGIC

#region Properties
	PROPERTY Surface		AS VOPanel	GET oSurfacePanel
	PROPERTY InitialSize    AS System.Drawing.Size AUTO GET SET
	PROPERTY IsShown		AS LOGIC AUTO GET PRIVATE SET
#endregion

	CONSTRUCTOR(oWindow AS Window, oRes AS ResourceDialog)
		SUPER(oWindow)
		SELF:oResDlg := oRes
		SELF:StartPosition	       := FormStartPosition.CenterParent
		SELF:Text                  := "DialogForm"
		IF oRes != NULL
			SELF:Size				:= oRes:Size
			SELF:InitialSize		:= oRes:Size
			SELF:lMustAdjust		:= _AND(oRes:Style, WS_SYSMENU|WS_MINIMIZEBOX|WS_MAXIMIZEBOX) != 0
		ELSE
			SELF:lMustAdjust := FALSE
			SELF:Size				:= System.Drawing.Size{1,1}
		ENDIF
		SELF:AutoSizeMode          := AutoSizeMode.GrowAndShrink

		oSurfacePanel              := GuiFactory.Instance:CreateSurfacePanel(oWindow)
		oSurfacePanel:Visible      := TRUE
		SELF:oSurfacePanel:Dock    := DockStyle.Fill
		SELF:Controls:Add(oSurfacePanel)



#region Winforms Method overrides

	OVERRIDE PROTECT METHOD OnShown(e AS EventArgs) AS VOID STRICT
		FOREACH oC AS System.Windows.Forms.Control IN SELF:Surface:Controls
			IF oC IS IVOButton VAR button
				IF button:DefaultButton
					SELF:AcceptButton := (IButtonControl) button
				ENDIF
			ENDIF
		NEXT
		SUPER:OnShown(e)
		LOCAL oFirst AS System.Windows.Forms.Control
		oFirst := SELF:GetFirstEditableControl()
		IF oFirst != NULL_OBJECT
			oFirst:Select()
		ENDIF
		((DialogWindow) Window):PostShowDialog()

	OVERRIDE PROTECTED PROPERTY CreateParams AS System.Windows.Forms.CreateParams
		GET
			LOCAL IMPLIED result := SUPER:CreateParams
			IF SELF:oResDlg != NULL_OBJECT
				SELF:oResDlg:CopyCreateParams(result, TRUE)
			ENDIF
			RETURN result
		END GET
	END PROPERTY


	OVERRIDE PROTECTED METHOD OnClosing( e AS CancelEventargs) AS VOID
		SUPER:OnClosing(e )

	OVERRIDE PROTECTED METHOD OnVisibleChanged( e AS System.EventArgs) AS VOID
		SUPER:OnVisibleChanged(e)
		IF SELF:Visible
			// Fix problem of group boxes that cover other controls
			SELF:oSurfacePanel:Prepare()
			IF ! SELF:IsShown
				SELF:oSurfacePanel:Dock			:= DockStyle.Fill
				// When the window is painted with style WS_SYSMENU|WS_MINIMIZEBOX|WS_MAXIMIZEBOXsometimes the size is too small
				IF lMustAdjust .AND. !SELF:InitialSize:IsEmpty .AND. (SELF:Width != SELF:InitialSize:Width .OR. SELF:Height != SELF:InitialSize:Height)
					SELF:Size := SELF:InitialSize
					IF SELF:StartPosition == System.Windows.Forms.FormStartPosition.CenterScreen
						SELF:CenterToScreen()
					ENDIF
				ENDIF
				SELF:IsShown := TRUE
			ENDIF
		ENDIF
	RETURN

	OVERRIDE PROTECTED METHOD OnSizeChanged(e AS EventArgs) AS VOID
		SUPER:OnSizeChanged(e)



#endregion


#region Methods to reproduce the VO DIalogwindow behavior
	METHOD GetFirstEditableControl AS System.Windows.Forms.Control
		RETURN GetFirstEditableChild(SELF:oSurfacePanel)


	METHOD SetSizable(lSet AS LOGIC) AS VOID
		IF lSet
			SELF:FormBorderStyle := FormBorderStyle.Sizable
		ELSE
			SELF:FormBorderStyle := FormBorderStyle.FixedDialog
		ENDIF


#endregion

END CLASS

