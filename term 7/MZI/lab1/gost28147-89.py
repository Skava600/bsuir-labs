from pygost import gost28147


def fit_key(key):
    if len(key) < 32:
        key += ' ' * (32 - len(key))
    if len(key) > 32:
        key = key[0:31]
    return key


def fit_data(data):
    return data + ' ' * (len(data) % 8)


def gost28147_89_encrypt(text, key):
    return gost28147.cbc_encrypt(key=bytes(fit_key(key), 'utf-8'), data=bytes(fit_data(text), 'utf-8'))


def gost28147_89_decrypt(text, key):
    return gost28147.cbc_decrypt(key=bytes(fit_key(key), 'utf-8'), data=text)


if __name__ == '__main__':
    key = 'Key 1111'
    text = ""
    with open("input.txt", "r") as file:
        text = file.read().replace('\n', '')
    gost28147_89_encrypted = gost28147_89_encrypt(text, key)
    gost28147_89_decrypted = gost28147_89_decrypt(gost28147_89_encrypted, key)
    with open("gost28147_89_encrypted.txt", "wb+") as file:
        file.write(gost28147_89_encrypted)
    print(gost28147_89_encrypted)
    with open("gost28147_89_decrypted.txt", "wb+") as file:
        file.write(gost28147_89_decrypted)
    print(gost28147_89_decrypted)

