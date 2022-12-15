import math
import  numpy as np
from numpy import Inf
class Canteen:
    def __init__(self, X, p, q, t):
        self.X = X
        self.p = p
        self.q = q
        self.t = t

    def ro(self):
        return (self.X * (self.q + 1)) / self.p

    def is_stationary(self):
        return self.X < self.p / (self.q + 1)

    # полное ожидание случайно величины Т
    def m_t(self):
        return (2 / self.p) * self.q + (1 - self.q) * 1 / self.p

    #   матожидание при первой гипотезе по показательному закону D(T/H1) + (M(T/H1))^2
    def m_t2_h1(self):
        return 1 / math.pow(self.p, 2) + math.pow(1 / self.p, 2)

    def m_t2_h2(self):
        return 1 / math.pow(self.p, 2) + math.pow(2 / self.p, 2)

    # второй начальный момент
    def m_t2(self):
        return (1-self.q) * self.m_t2_h1() + self.q * self.m_t2_h2()

    def d_t(self):
        return self.m_t2() - self.m_t() * self.m_t()

    #  коэф вариации
    def v(self):
        return math.sqrt(self.d_t()) / self.m_t()

    def get_average_queue_length_stat(self):
        return math.pow(self.ro(), 2) * (1 + math.pow(self.v(), 2)) /( 2 * (1 - self.ro()))

    def get_average_stay_time_stat(self):
        return (self.get_average_queue_length_stat() / self.X) + (self.q + 1) / self.p + (self.q + 1) * self.t


def get_canteen_alpha(X, mu, q):
    #alpha = X(2q + (1 - q)) / mu = X(1 + q) / mu
    return X * (1 + q) / mu


def canteen_has_static_state(X, mu, q):
    #alpha = X(2q + (1 - q)) / mu = X(1 + q) / mu
    return get_canteen_alpha(X, mu, q) < 1


# Calculate Theoretical Characteristics
def calculate_theoretical_characteristics(X, mu, q, t):
    alpha = get_canteen_alpha(X, mu, q)
    p1=[]
    p2=[]
    p=[]
    Q = None
    A = None
    m_eating = X * (1 + q) * t
    if canteen_has_static_state(X, mu, q):
        for i in range(30):
            p1.append((alpha ** i) * (1 - alpha))
            # p2.append(1 - np.exp(- i / m_eating))
            # p2.append(1 - (alpha ** i) / math.factorial(i) * np.exp(-1 / m_eating))
            # p2.append(0 if i == 0 else np.exp(- (i - 1) / m_eating) - np.exp(- i / m_eating))
            # p2.append(np.exp(- i / m_eating) - np.exp(- (i + 1) / m_eating))
            F_x = lambda x: 1 - np.exp(- x / m_eating)
            if i == 0:
                p2.append(F_x(0.5))
            else:
                p2.append(F_x(i + 0.5) - F_x(i - 0.5))
            indexes_i = [(j, i - j) for j in range(i + 1)]
            p_i = 0
            for j in range(len(indexes_i)):
                p_i = p_i + p1[indexes_i[j][0]] * p2[indexes_i[j][1]]
            p.append(p_i)
        Q = 1
        A = X
        L_canteen = alpha / (1 - alpha) + m_eating
        L_queue = alpha ** 2 / (1 - alpha)
        t_canteen = L_canteen / X
        t_queue = L_queue / X
    else:
        p=[0 for _ in range(15)]
        Q = 1
        A = X
        L_canteen = Inf
        L_queue = Inf
        t_canteen = Inf
        t_queue = Inf

    return p, Q, A, L_canteen, L_queue, t_canteen, t_queue