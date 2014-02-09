#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="certutil.h" company="Microsoft">
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
//    Certificate helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

HRESULT DAPI CertReadProperty(
    __in PCCERT_CONTEXT pCertContext,
    __in DWORD dwProperty,
    __deref_out_bound LPVOID pvValue
    );

HRESULT DAPI GetCryptProvFromCert(
      __in_opt HWND hwnd,
      __in PCCERT_CONTEXT pCert,
      __out HCRYPTPROV *phCryptProv,
      __out DWORD *pdwKeySpec,
      __in BOOL *pfDidCryptAcquire,
      __deref_opt_out LPWSTR *ppwszTmpContainer,
      __deref_opt_out LPWSTR *ppwszProviderName,
      __out DWORD *pdwProviderType
      );

HRESULT DAPI FreeCryptProvFromCert(
    __in BOOL fAcquired,
    __in HCRYPTPROV hProv,
    __in_opt LPWSTR pwszCapiProvider,
    __in DWORD dwProviderType,
    __in_opt LPWSTR pwszTmpContainer
    );

HRESULT DAPI GetProvSecurityDesc(
      __in HCRYPTPROV hProv,
      __deref_out SECURITY_DESCRIPTOR** pSecurity
      );

HRESULT DAPI SetProvSecurityDesc(
    __in HCRYPTPROV hProv,
    __in SECURITY_DESCRIPTOR* pSecurity
    );

BOOL DAPI CertHasPrivateKey(
    __in PCCERT_CONTEXT pCertContext,
    __out_opt DWORD* pdwKeySpec
    );

HRESULT DAPI CertInstallSingleCertificate(
    __in HCERTSTORE hStore,
    __in PCCERT_CONTEXT pCertContext,
    __in LPCWSTR wzName
    );
#ifdef __cplusplus
}
#endif
