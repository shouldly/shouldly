#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="memutil.h" company="Microsoft">
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
//    Header for memory helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

#define ReleaseMem(p) if (p) { MemFree(p); }
#define ReleaseNullMem(p) if (p) { MemFree(p); p = NULL; }

#define MEM_ENSURE_ARRAY_SIZE(type, pointer, count, max, grow, hresult, errMsg) \
    if (max <= count) { \
        LPVOID pv = NULL; DWORD cNewMax = count + grow; \
        if (0 == max) pv = MemAlloc(sizeof(type) * cNewMax, TRUE); else pv = MemReAlloc(pointer, sizeof(type) * cNewMax, TRUE); \
        ExitOnNull(pv, hresult, E_OUTOFMEMORY, errMsg); \
        max = cNewMax; pointer = static_cast<type*>(pv); \
    }

HRESULT DAPI MemInitialize();
void DAPI MemUninitialize();

LPVOID DAPI MemAlloc(
    __in SIZE_T cbSize,
    __in BOOL fZero
    );
LPVOID DAPI MemReAlloc(
    __in LPVOID pv,
    __in SIZE_T cbSize,
    __in BOOL fZero
    );

HRESULT DAPI MemEnsureArraySize(
    __inout_ecount(cArray) LPVOID* ppvArray,
    __in DWORD cArray,
    __in SIZE_T cbArrayType,
    __in DWORD dwGrowthCount
    );

HRESULT DAPI MemFree(
    __in LPVOID pv
    );
SIZE_T DAPI MemSize(
    __in LPCVOID pv
    );

#ifdef __cplusplus
}
#endif

