import pyDes

def double_des_encrypt(text, key1, key2):
    key1 = pyDes.des(key1, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    key2 = pyDes.des(key2, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    return key2.encrypt(key1.encrypt(text))


def double_des_decrypt(text, key1, key2):
    key1 = pyDes.des(key1, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    key2 = pyDes.des(key2, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    return key1.decrypt(key2.decrypt(text))


def triple_des_encrypt(text, key1, key2, key3):
    key1 = pyDes.des(key1, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    key2 = pyDes.des(key2, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    key3 = pyDes.des(key3, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    return key3.encrypt(key2.encrypt(key1.encrypt(text)))


def triple_des_decrypt(text, key1, key2, key3):
    key1 = pyDes.des(key1, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    key2 = pyDes.des(key2, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    key3 = pyDes.des(key3, pyDes.CBC, b"\0\0\0\0\0\0\0\0", pad=None, padmode=pyDes.PAD_PKCS5)
    return key1.decrypt(key2.decrypt(key3.decrypt(text)))


if __name__ == '__main__':
    key1 = 'KEY 1112'
    key2 = 'KEY 2222'
    key3 = 'KEY 3333'
    text = ""
    with open("input.txt", "r") as file:
        text = file.read().replace('\n', '')
    double_des_encrypted = double_des_encrypt(text, key1, key2)
    double_des_decrypted = double_des_decrypt(double_des_encrypted, key1, key2)
    with open("double_des_encrypted.txt", "wb+") as file:
        file.write(double_des_encrypted)
    with open("double_des_decrypted.txt", "wb+") as file:
        file.write(double_des_decrypted)
    triple_des_encrypted = triple_des_encrypt(text, key1, key2, key3)
    triple_des_decrypted = triple_des_decrypt(triple_des_encrypted, key1, key2, key3)
    with open("triple_des_encrypted.txt", "wb+") as file:
        file.write(triple_des_encrypted)
    with open("triple_des_decrypted.txt", "wb+") as file:
        file.write(triple_des_decrypted)

