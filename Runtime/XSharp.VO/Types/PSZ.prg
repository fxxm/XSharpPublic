﻿//
// Copyright (c) XSharp B.V.  All Rights Reserved.  
// Licensed under the Apache License, Version 2.0.  
// See License.txt in the project root for license information.
//
USING System.Collections.Generic
USING System.Runtime.InteropServices
USING System.Diagnostics
USING System.Text
USING System.Reflection
BEGIN NAMESPACE XSharp
	
	/// <summary>Internal type that implements the VO Compatible PSZ type.<br/>
	/// This type has many operators and implicit converters that normally are never directly called from user code.
	/// </summary>
	[DebuggerDisplay( "{DebuggerString(),nq}", Type := "PSZ" ) ] ;
	STRUCTURE __Psz IMPLEMENTS  IEquatable<__Psz>
		PRIVATE _value AS BYTE PTR
		/// <exclude />	
		STATIC PROPERTY _NULL_PSZ AS __Psz GET (__Psz) IntPtr.zero
		PRIVATE STATIC _pszList AS List< IntPtr>
		INTERNAL STATIC METHOD RegisterPsz(pszToRegister AS PSZ) AS VOID
			IF _pszList == NULL
				_pszList := List<IntPtr>{}
				AppDomain.CurrentDomain:ProcessExit += System.EventHandler{ NULL, @__FreePSZs() }
			ENDIF
			IF !_pszList:Contains(pszToRegister:Address)
				_pszList:add(pszToRegister:Address)
			ENDIF
			RETURN
		
		INTERNAL STATIC METHOD CreatePsz( cString AS STRING) AS PSZ
				RETURN PSZ{cString}
		
		PRIVATE STATIC METHOD __FreePSZs(o AS OBJECT, args AS EventArgs ) AS VOID
			FOREACH VAR pszToFree IN _pszList
				TRY
					MemFree(pszToFree)
				CATCH
					NOP
				END TRY
			NEXT
			_pszList := NULL
		
		/// <summary>This constructor is used in code generated by the compiler when needed.</summary>
		CONSTRUCTOR (s AS STRING)
			// this constructor has a memory leak
			// there is no garbage collection for structures
			// to free the memory we need to call MemFree on the pointer
			_value := String2Mem(s)
			RegisterPsz(_value)
			RETURN
		
		/// <summary>This constructor is used in code generated by the compiler when needed.</summary>
		CONSTRUCTOR (p AS IntPtr)
			_value := p
		
		/// <exclude/>
        OVERRIDE METHOD ToString() AS STRING
			RETURN Mem2String( _value, Length ) 
	
		/// <exclude />	
		METHOD DebuggerString() AS STRING
			RETURN IIF( _value == NULL_PTR, "NULL_PSZ", e"\""+ tostring() +  e"\"" )
		
		
		/// <exclude />	
		METHOD @@Equals( p AS PSZ ) AS LOGIC
			
			LOCAL ret := FALSE AS LOGIC
			IF _value == p:_value
				ret := TRUE
			ELSEIF _value != NULL && p:_value != NULL
				ret := String.CompareOrdinal( ToString(), p:ToString() ) == 0
			ENDIF
			RETURN ret   
		
		INTERNAL METHOD LessThan( p AS PSZ ) AS LOGIC
			// todo: should this follow nation rules ?
			LOCAL ret := FALSE AS LOGIC
			IF _value == p:_value
				ret := FALSE
			ELSEIF _value != NULL && p:_value != NULL
				ret := String.CompareOrdinal( ToString(), p:ToString() ) < 0
			ENDIF
			RETURN ret       

		INTERNAL METHOD GreaterThan( p AS PSZ ) AS LOGIC
			LOCAL ret := FALSE AS LOGIC
			// todo: should this follow nation rules ?
			IF _value == p:_value
				ret := FALSE
			ELSEIF _value != NULL && p:_value != NULL
				ret := String.CompareOrdinal( ToString(), p:ToString() ) > 0
			ENDIF
			RETURN ret     
		
		
			/// <exclude/>
            OVERRIDE METHOD EQUALS( o AS OBJECT ) AS LOGIC
			LOCAL ret := FALSE AS LOGIC
			
			IF o IS PSZ
				ret := SELF:Equals( (PSZ) o )
			ENDIF
			
		RETURN ret
		
		/// <inheritdoc />
		OVERRIDE METHOD GetHashCode() AS INT
			RETURN (INT) _value
		
		/// <exclude />	
		METHOD Free() AS VOID
			IF _value != NULL_PTR
				MemFree( _value )
				_value := NULL_PTR
			ENDIF
			RETURN
		/// <exclude />
		PROPERTY Length AS DWORD
			GET
				LOCAL len AS DWORD
				len := 0
				IF _value != NULL_PTR
					DO WHILE _value[len+1] != 0
						len++
					ENDDO
				ENDIF
				RETURN len 
			END GET
		END PROPERTY
		/// <exclude />
		PROPERTY IsEmpty AS LOGIC
			GET
				LOCAL empty := TRUE AS LOGIC
				LOCAL b AS BYTE
				LOCAL x := 1 AS INT
				IF _value != NULL_PTR
					b := _value[x]
					DO WHILE b != 0 .AND. empty
						SWITCH b
							CASE 32
							CASE 13
							CASE 10
							CASE 9
								NOP
							OTHERWISE
								empty := FALSE
						END SWITCH
						x += 1
						b := _value[x]
					ENDDO
				ENDIF
				RETURN empty
				
				
			END GET
		END PROPERTY
		/// <exclude />
		PROPERTY IsNull AS LOGIC GET _value == NULL
		/// <exclude />
		PROPERTY Address AS IntPtr GET _value
		/// <exclude />
		PROPERTY Item[index AS INT] AS BYTE
			GET
				RETURN _value[index]
			END GET
			SET
				_value[index] := VALUE
			END SET
		END PROPERTY
		
		#region OPERATOR methods
			// binary
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR +( lhs AS PSZ, rhs AS PSZ ) AS PSZ
				RETURN PSZ{ lhs:ToString() + rhs:ToString() }
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR +( lhs AS PSZ, rhs AS STRING ) AS PSZ
				RETURN PSZ{ lhs:ToString() + rhs }
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR +( lhs AS STRING, rhs AS PSZ ) AS STRING
				RETURN lhs + rhs:ToString()
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR -( lhs AS PSZ, rhs AS PSZ ) AS PSZ
				LOCAL l   := lhs:ToString() AS STRING
				LOCAL r   := rhs:ToString() AS STRING
				RETURN PSZ{ String.Concat( l:TrimEnd(), r:TrimEnd() ):PadRight( l:Length + r:Length ) }
			
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR -( lhs AS PSZ, rhs AS STRING ) AS PSZ
				LOCAL l   := lhs:ToString() AS STRING
				RETURN PSZ{ String.Concat( l:TrimEnd(), rhs:TrimEnd() ):PadRight( l:Length + rhs:Length ) }
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR -( lhs AS STRING, rhs AS PSZ ) AS STRING
				LOCAL r   := rhs:ToString() AS STRING
				RETURN String.Concat( lhs:TrimEnd(), r:TrimEnd() ):PadRight( lhs:Length + r:Length )
			
			// Comparison Operators
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR ==( lhs AS PSZ, rhs AS PSZ ) AS LOGIC
				RETURN lhs:Equals( rhs )
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR !=( lhs AS PSZ, rhs AS PSZ ) AS LOGIC
				RETURN ! lhs:Equals( rhs )
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR <( lhs AS PSZ, rhs AS PSZ ) AS LOGIC
				RETURN lhs:LessThan( rhs )
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR <=( lhs AS PSZ, rhs AS PSZ ) AS LOGIC
				RETURN ! lhs:GreaterThan( rhs )
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR >( lhs AS PSZ, rhs AS PSZ ) AS LOGIC
				RETURN lhs:GreaterThan( rhs )
			
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR >=( lhs AS PSZ, rhs AS PSZ ) AS LOGIC
				RETURN ! lhs:LessThan( rhs )
			
			// Conversion Operators - To PSZ...  
			
			// PTR -> PSZ
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PTR ) AS PSZ
				RETURN PSZ{ (IntPtr) p }
			
			// BYTE PTR -> PSZ
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS BYTE PTR ) AS PSZ
				RETURN PSZ{ (IntPtr) p }
			
			// SByte PTR -> PSZ
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS SByte PTR ) AS PSZ
				RETURN PSZ{ (IntPtr) p }
			
			// IntPtr -> PSZ
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS IntPtr ) AS PSZ
				RETURN PSZ{ p }
			
			// INT -> PSZ
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( i AS INT ) AS PSZ
				RETURN PSZ{ IntPtr{ i } }
			
			// DWORD -> PSZ
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( d AS DWORD ) AS PSZ
				RETURN PSZ{ IntPtr{ (PTR) d } }
			
			///////////////////////////////////////////////////////////////////////////
			// Conversion Operators - From PSZ...  
			
			// PSZ -> PTR
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PSZ ) AS PTR
				RETURN p:_value
			
			// PSZ -> BYTE PTR
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PSZ ) AS BYTE PTR
				RETURN p:_value
			
			// PSZ -> SByte PTR
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PSZ ) AS SByte PTR
				RETURN (SByte PTR) p:_value
			
			// PSZ -> IntPtr
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PSZ ) AS IntPtr
				RETURN p:_value
			
			// PSZ -> STRING
			OPERATOR IMPLICIT( p AS PSZ ) AS STRING
				RETURN p:ToString()
			
			// PSZ -> INT
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PSZ ) AS INT
				RETURN (INT) p:_value
			
			// PSZ -> INT64
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PSZ ) AS INT64
				RETURN (INT64) p:_value
			
			// PSZ -> DWORD
			/// <summary>This operator is used in code generated by the compiler when needed.</summary>
			OPERATOR IMPLICIT( p AS PSZ ) AS DWORD
				RETURN (DWORD) p:_value			
		#endregion
		
		
	END STRUCTURE
	
END NAMESPACE




// This function is handled by the compiler. The runtime function should never be called
/// <exclude />
FUNCTION Cast2Psz(cSource AS STRING) AS PSZ
	THROW NotImplementedException{}

// This function is handled by the compiler. The runtime function should never be called
/// <exclude />
FUNCTION String2Psz(cSource AS STRING) AS PSZ
	THROW NotImplementedException{}

