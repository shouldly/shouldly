#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="jsonutil.h" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
//    
//    The use and distribution terms for this software are covered by the
//    Common Public License 1.0 (http://opensource.org/licenses/cpl1.0.php)
//    which can be found in the file CPL.TXT at the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by
//    the terms of this license.
//    
//    You must not remove this notice, or any other, from this software.
// </copyright>
// 
// <summary>
//    JavaScript Object Notation (JSON) helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

enum JSON_WRITER_TOKEN
{
    JSON_WRITER_TOKEN_NONE,
    JSON_WRITER_TOKEN_ARRAY_START,
    JSON_WRITER_TOKEN_ARRAY_VALUE,
    JSON_WRITER_TOKEN_ARRAY_END,
    JSON_WRITER_TOKEN_OBJECT_START,
    JSON_WRITER_TOKEN_OBJECT_KEY,
    JSON_WRITER_TOKEN_OBJECT_VALUE,
    JSON_WRITER_TOKEN_OBJECT_END,
    JSON_WRITER_TOKEN_VALUE,
};

typedef struct _JSON_WRITER
{
    CRITICAL_SECTION cs;
    LPWSTR sczJson;

    JSON_WRITER_TOKEN* rgTokenStack;
    DWORD cTokens;
    DWORD cMaxTokens;
} JSON_WRITER;


DAPI_(HRESULT) JsonInitializeWriter(
    __in JSON_WRITER* pWriter
    );

DAPI_(void) JsonUninitializeWriter(
    __in JSON_WRITER* pWriter
    );

DAPI_(HRESULT) JsonWriteBool(
    __in JSON_WRITER* pWriter,
    __in BOOL fValue
    );

DAPI_(HRESULT) JsonWriteNumber(
    __in JSON_WRITER* pWriter,
    __in DWORD dwValue
    );

DAPI_(HRESULT) JsonWriteString(
    __in JSON_WRITER* pWriter,
    __in_z_opt LPCWSTR wzValue
    );

DAPI_(HRESULT) JsonWriteArrayStart(
    __in JSON_WRITER* pWriter
    );

DAPI_(HRESULT) JsonWriteArrayEnd(
    __in JSON_WRITER* pWriter
    );

DAPI_(HRESULT) JsonWriteObjectStart(
    __in JSON_WRITER* pWriter
    );

DAPI_(HRESULT) JsonWriteObjectKey(
    __in JSON_WRITER* pWriter,
    __in_z LPCWSTR wzKey
    );

DAPI_(HRESULT) JsonWriteObjectEnd(
    __in JSON_WRITER* pWriter
    );

#ifdef __cplusplus
}
#endif
