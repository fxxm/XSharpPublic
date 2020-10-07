﻿//
// Copyright (c) XSharp B.V.  All Rights Reserved.  
// Licensed under the Apache License, Version 2.0.  
// See License.txt in the project root for license information.
//

// Extension methods to the FileStream class that capture exceptions and return "smart" values so the calling code 
// does not have to set exception handlers and looks cleaner

USING System
USING System.Collections.Generic
USING System.Text
USING System.IO

BEGIN NAMESPACE XSharp
    STATIC CLASS FileStreamExensions
    
        STATIC METHOD SafeRead(SELF oStream AS FileStream, buffer AS BYTE[]) AS LOGIC
            RETURN SafeRead(oStream, buffer, buffer:Length)
            
        STATIC METHOD SafeRead(SELF oStream AS FileStream, buffer AS BYTE[], length AS LONG) AS LOGIC
            LOCAL result AS LONG
            TRY
                result := oStream:Read(buffer, 0, length)
            CATCH
                result := -1
            END TRY
            RETURN result == length
            
        STATIC METHOD SafeRead(SELF oStream AS FileStream, buffer AS BYTE[], length AS LONG, lread OUT LONG) AS LOGIC
            LOCAL result AS LOGIC
            lread := -1
            TRY
                lread := oStream:Read(buffer, 0, length)
                result := TRUE
            CATCH
                result := FALSE
            END TRY
            RETURN result
        STATIC METHOD SafeWrite(SELF oStream AS FileStream, buffer AS BYTE[]) AS LOGIC
            RETURN SafeWrite(oStream, buffer, buffer:Length)
            
        STATIC METHOD SafeWrite(SELF oStream AS FileStream, buffer AS BYTE[], length AS LONG) AS LOGIC
            LOCAL result AS LOGIC
            TRY
                oStream:Write(buffer, 0, length)
                result := TRUE
            CATCH
                result := FALSE
            END TRY
            RETURN result
         STATIC METHOD SafeWriteByte(SELF oStream AS FileStream, b AS BYTE) AS LOGIC
            LOCAL result AS LOGIC
            TRY
                oStream:WriteByte(b)
                result := TRUE
            CATCH
                result := FALSE
            END TRY
            RETURN result
         STATIC METHOD SafeSetPos(SELF oStream AS FileStream, offset AS INT64) AS LOGIC
            LOCAL result AS LOGIC
            TRY
                result := oStream:Seek(offset, SeekOrigin.Begin) == offset
            CATCH
                result := FALSE
            END TRY
            RETURN result
         STATIC METHOD SafeLock(SELF oStream AS FileStream, offset AS INT64, length AS INT64) AS LOGIC
           LOCAL result AS LOGIC
            TRY
                oStream:Lock(offset, length)
                result := TRUE
            CATCH
                result := FALSE
            END TRY
            RETURN result
         STATIC METHOD SafeUnlock(SELF oStream AS FileStream, offset AS INT64, length AS INT64) AS LOGIC
           LOCAL result AS LOGIC
            TRY
                oStream:Unlock(offset, length)
                result := TRUE
            CATCH
                result := FALSE
            END TRY
            RETURN result

        STATIC METHOD SafeSetLength(SELF oStream AS FileStream, length AS INT64) AS LOGIC
           LOCAL result AS LOGIC
            TRY
                oStream:SetLength(length)
                result := TRUE
            CATCH
                result := FALSE
            END TRY
            RETURN result 
    END CLASS
END NAMESPACE    
