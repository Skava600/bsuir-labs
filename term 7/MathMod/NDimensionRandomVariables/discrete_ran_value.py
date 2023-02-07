from scipy.stats import rv_discrete
import numpy as np
import matplotlib.pyplot as plt
import math
import random
import sys
import csv
from matplotlib import cm
from random import random
from scipy import integrate
from scipy import stats

def generate_dsv(x, y, P):
    n, m = len(x), len(y)
    q = [sum(P[:, i]) for i in range(n)]
    Fx = [sum(q[:k + 1]) for k in range(n)]

    x_i = 0
    e = random()
    while e > Fx[x_i]:
        x_i += 1

    Fy = [sum(P[:k + 1, x_i]) for k in range(m)]

    y_i = 0
    e = random() * max(Fy)
    while e > Fy[y_i]:
        y_i += 1

    return x[x_i], y[y_i]


x = [1, 2, 3, 4, 5]
y = [6, 7, 8, 9, 10]

n = len(x)
m = len(y)

P = np.array(
    [[0.05, 0.01, 0.05, 0.03, 0.10],
     [0.09, 0.03, 0.06, 0.14, 0.04],
     [0.05, 0.01, 0.03, 0.01, 0.05],
     [0.03, 0.01, 0.01, 0.03, 0.04],
     [0.04, 0.03, 0.03, 0.02, 0.01]])

print("Сумма элементов матрицы: ", np.sum(P))
if round(np.sum(P), 3) != 1.000:
    print("Матрица распределения ДСВ задана неверно! Дальнейшая работа невозможна")
    sys.exit()


N = 10000

x_values = []
y_values = []

for _ in range(N):
    _x, _y = generate_dsv(x, y, P)
    x_values.append(_x)
    y_values.append(_y)

p_x = [sum(P[:, i]) for i in range(n)]
p_y = [sum(P[j, :]) for j in range(m)]


p_x_imp = [x_values.count(_x) / N for _x in x]
p_y_imp = [y_values.count(_y) / N for _y in y]

print(f"{p_x}\n{p_x_imp}\n")
print(f"{p_y}\n{p_y_imp}\n")

def independence(P, p_x, p_y):
    for i in range(len(p_x)):
        for j in range(len(p_y)):
            if P[i][j] != p_x[i] * p_y[j]:
                print('X, Y зависимы')
                print(f'{P[i][j]} != {p_x[i]*p_y[j]}')
                return
    print('X, Y независимы')

independence(P, p_x, p_y)

p_yx = np.copy(P)
for i in range(n):
    for j in range(m):
        p_yx[j, i] /= p_x[i]

print("P(Y|X)\n", p_yx)

p_xy = np.copy(P)
for i in range(n):
    for j in range(m):
        p_xy[j, i] /= p_y[j]

print("P(X|Y)\n", p_xy)

plt.hist(x_values, weights=[1/N]*N, color="red")
plt.plot(x, p_x)
plt.hist(y_values, weights=[1/N]*N, color="blue")
plt.plot(y, p_y)
plt.show()

alpha = 0.99

def stat_mean(x):
    return sum(x) / len(x)

def stat_variance(x, mean):
    return sum((_x - mean) ** 2 for _x in x) / len(x)

def stat_correlation(x, y, Mx, My):
    numerator = sum((_x - Mx) * (_y - My) for _x, _y in zip(x, y))
    sum_x2 = sum((_x - Mx) ** 2 for _x in x)
    sum_y2 = sum((_y - My) ** 2 for _y in y)
    return numerator / np.sqrt(sum_x2 * sum_y2)

def discrete_mean(x, p_x):
    return sum([x[i] * p_x[i] for i in range(len(x))])

def discrete_variate(x, p_x, M):
    return sum([(x[i] ** 2) * p_x[i] for i in range(len(x))]) - M ** 2

#Стандартное отклонение
def sem(x, s2):
    return np.sqrt(s2 / len(x))

# Исправленная выборочная дисперсия
def s2(x, Mx):
    return sum((_x - Mx) ** 2 for _x in x) / (len(x) - 1)

def ci_mean(x, alpha):
    m = stat_mean(x)
    se = sem(x, s2(x, m))
    return m - alpha * se, m + alpha * se

def ci_variance(x, alpha):
    a = (1 - alpha) / 2
    n = len(x)
    _s2 = s2(x, stat_mean(x))
    left = (n - 1) * _s2 / stats.chi2.ppf(1 - a, df=n - 1)
    right = (n - 1) * _s2 / stats.chi2.ppf(a, df=n - 1)
    return left, right

Mx = discrete_mean(x, p_x)
My = discrete_mean(y, p_y)
Dx = discrete_variate(x, p_x, Mx)
Dy = discrete_variate(y, p_y, My)
Mxy = sum([sum([x * y * P[j, i] for j, y in enumerate(y)]) for i, x in enumerate(x)])
correlation = (Mxy - Mx * My) / np.sqrt(Dx * Dy)

print('Теоретическое значение m[x]', Mx)
print('Теоретическое значение m[y]', My)
print('Теоретическое значение D[y]', Dy)
print('Теоретическое значение D[x]', Dx)
print('Теоретическое значение коэффициента корреляции', correlation)

Mx_imp = discrete_mean(x, p_x_imp)
My_imp = discrete_mean(y, p_y_imp)
Dx_imp = discrete_variate(x, p_x_imp, Mx_imp)
Dy_imp = discrete_variate(y, p_y_imp, My_imp)
corr_imp = stat_correlation(x_values, y_values, Mx_imp, My_imp)

print('Точечная оценка m[y]', My_imp)
print('Точечная оценка m[x]', Mx_imp)
print('Точечная оценка D[y]', Dy_imp)
print('Точечная оценка D[x]', Dx_imp)
print('Точечная оценка коэффициента корреляции', corr_imp)

print('Доверительный интервал мат. ожидания X', ci_mean(x_values, alpha))
print('Доверительный интервал мат. ожидания Y', ci_mean(y_values, alpha))
print('Доверительный интервал дисперсии X', ci_variance(x_values, alpha))
print('Доверительный интервал дисперсии Y', ci_variance(y_values, alpha))

print("Коэффициент корреляции R[xy]: ", correlation)