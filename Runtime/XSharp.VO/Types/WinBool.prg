﻿//
// Copyright (c) XSharp B.V.  All Rights Reserved.  
// Licensed under the Apache License, Version 2.0.  
// See License.txt in the project root for license information.
//
USING System.Collections
USING System.Collections.Generic
USING System.Linq
USING System.Diagnostics

BEGIN NAMESPACE XSharp	
	/// <summary>Internal type that implements the WIN32 Compatible LOGIC type in UNIONs and VOSTRUCTs</summary>
    [DebuggerDisplay("{ToString(),nq}", Type := "LOGIC")];
    PUBLIC STRUCT __WinBool 
        PRIVATE STATIC trueValue := __WinBool{1}	AS __WinBool
        PRIVATE STATIC falseValue := __WinBool{0}	AS __WinBool
        PRIVATE _value AS INT			// 0 = false, 1 = true
        
        PRIVATE CONSTRUCTOR(VALUE AS INT)
            _value := VALUE
            
       	/// <summary>This constructor is used in code generated by the compiler when needed.</summary>
        CONSTRUCTOR (lValue AS LOGIC)
            _value := IIF(lValue, 1, 0)

		/// <inheritdoc />
        VIRTUAL METHOD GetHashCode() AS INT
            RETURN _value:GetHashCode()
            
       /// <exclude />
	    METHOD GetTypeCode() AS TypeCode
            RETURN TypeCode.Boolean
            
            
            #region Unary Operators
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
		 STATIC OPERATOR !(wb AS __WinBool) AS LOGIC
            RETURN wb:_value == 0
            #endregion
            
        #region Binary Operators
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        OPERATOR == (lhs AS __WinBool, rhs AS __WinBool) AS LOGIC
            RETURN lhs:_value == rhs:_value
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        OPERATOR != (lhs AS __WinBool, rhs AS __WinBool) AS LOGIC
            RETURN lhs:_value != rhs:_value
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        OPERATOR == (lhs AS __WinBool, rhs AS LOGIC) AS LOGIC
            RETURN IIF (rhs, lhs:_value != 0, lhs:_value == 0)
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        OPERATOR != (lhs AS __WinBool, rhs AS LOGIC) AS LOGIC
            RETURN IIF (rhs, lhs:_value == 0, lhs:_value != 0)
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        OPERATOR == (lhs AS LOGIC, rhs AS __WinBool) AS LOGIC
            RETURN IIF (lhs, rhs:_value != 0, rhs:_value == 0)
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        OPERATOR != (lhs AS LOGIC, rhs AS __WinBool) AS LOGIC
            RETURN IIF (lhs, rhs:_value == 0, rhs:_value != 0)
            
        PUBLIC METHOD EQUALS(obj AS OBJECT) AS LOGIC
            IF obj IS __WinBool
                RETURN SELF:_value == ((__WinBool) obj):_value
            ENDIF
            RETURN FALSE
            #endregion 
            
        #region Implicit Converters
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR IMPLICIT(wb AS __WinBool) AS LOGIC
            RETURN wb:_value != 0
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR IMPLICIT(u AS USUAL) AS __WinBool
            RETURN __WinBool{(LOGIC) u}
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR IMPLICIT(l AS LOGIC) AS __WinBool
            RETURN IIF(l, trueValue, falseValue)
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR IMPLICIT(wb AS __WinBool) AS USUAL
            RETURN wb:_value != 0
            
            #endregion
        #region Logical operators
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR &(lhs AS __WinBool, rhs AS __WinBool) AS __WinBool
            RETURN IIF( lhs:_value == 1 .AND. rhs:_value == 1, trueValue, falseValue)
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR |(lhs AS __WinBool, rhs AS __WinBool) AS __WinBool
            RETURN IIF(lhs:_value == 1 .OR. rhs:_value == 1, trueValue, falseValue)
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR TRUE(wb AS __WinBool ) AS LOGIC
            RETURN wb:_value == 1
            
       	/// <summary>This operator is used in code generated by the compiler when needed.</summary>
        STATIC OPERATOR FALSE(wb AS __WinBool )AS LOGIC
            RETURN wb:_value == 0
            
            #endregion
    END	STRUCT
END NAMESPACE