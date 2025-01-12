# cya-maui

## Create a new private key
`openssl genrsa -out privatekey.key 2048`

## Use key to create CertificateSigningRequest.csr for uploading to create a certificate.cer
`openssl req -new -key privatekey.key -out CertificateSigningRequest.csr`

## Generate certificate.p12 using certificate.cer
`openssl pkcs12 -export -out certificate.p12 -inkey privatekey.key -in certificate.cer`

## Convert certificate.p12 to text variable
`openssl base64 -in certificate.p12 -out IOS_CERTIFICATE.txt`

## Convert cya.mobileprovision profile to text variable
`openssl base64 -in cya.mobileprovision -out IOS_PROVISIONING_PROFILE.txt`

## Password
`Nsiimbi@2025`