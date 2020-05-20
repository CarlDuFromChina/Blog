export function encryptPwd(str) {
  const cryptoJS = require('crypto-js');
  const key = cryptoJS.enc.Utf8.parse('C0536798-3187-47F3-BF34-95596C9338BA'.substr(0, 32));
  const iv = cryptoJS.enc.Utf8.parse('33998425-3944-4DDE-B3C4-8CAA1681A1B4'.substr(0, 16));
  const options = {
    mode: cryptoJS.mode.CBC,
    padding: cryptoJS.pad.Pkcs7,
    iv: iv
  };
  const ciphertext = cryptoJS.AES.encrypt(str, key, options).toString();
  return ciphertext;
}
