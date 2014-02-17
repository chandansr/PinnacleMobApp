

/*
=========================================================================================================
SCRIPT        : SECURITY
CREATED BY    : SUNIL SHARMA
CREATION DATE : 14 SEP 2012
DESCRIPTION    :............
=========================================================================================================  
*/
// SETTING UP THE FORM
//------------------REGION STARTS--------------------
function fn_security() {
    var funthis = this;
    funthis.encrypt = function (setting) {
        var key = CryptoJS.enc.Utf8.parse($('[data-encyptionkey]').data('encyptionkey'));
        var iv = key;
        var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(setting.value), key,
            {
                keySize: 128 / 8,
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });

        var encryptedstr = encrypted.ciphertext.toString(CryptoJS.enc.Base64);
        //var decrypted = CryptoJS.AES.decrypt(encrypted, key, {
        //    keySize: 128 / 8,
        //    iv: iv,
        //    mode: CryptoJS.mode.CBC,
        //    padding: CryptoJS.pad.Pkcs7
        //});
        //alert('Encrypted :' + encrypted);
        //alert('Key :' + encrypted.key);
        //alert('Salt :' + encrypted.salt);
        //alert('iv :' + encrypted.iv);
        // alert('Decrypted : ' + decrypted);
        //alert('utf8 = ' + decrypted.toString(CryptoJS.enc.Utf8));
        //return decrypted.toString(CryptoJS.enc.Utf8);  

        //if (setting.urlencode != undefined) {
        //    if (setting.urlencode) {
        //        encryptedstr = BaseString64.encodeURI(encryptedstr);
        //        debugger;
        //    }
        //}
        return encryptedstr;
    }
    funthis.decrypt = function (setting) {
        //return Aes.Ctr.decrypt(setting.value, 256);

        var key = CryptoJS.enc.Utf8.parse('9757135457251847');
        var iv = key;

        var decrypted = CryptoJS.AES.decrypt(setting.value, key, {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });
        
        return decrypted.toString(CryptoJS.enc.Utf8);
    }
}
//------------------REGION ENDS----------------------
