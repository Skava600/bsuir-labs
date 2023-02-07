import math
import random
from f_density import FDenstity

import numpy as np
from matplotlib import pyplot as plt
from mpl_toolkits import mplot3d

import sympy as sp
import scipy
from math import e

from IPython.display import display as ipydisplay, Math


# Плотность распределения
f_str = 'exp(-x-y)'
f_sp = sp.sympify(f_str)
f = sp.lambdify(sp.symbols('x, y'), f_sp)
x_sp, y_sp = sp.symbols('x y')

print(f'f(x, y) = {f_sp}')

f_xy = FDenstity(f, 0, math.inf, 0, math.inf)


# Функция распределения

F_sp = sp.integrate(
    f_sp,
    (x_sp, f_xy.x1, x_sp),
    (y_sp, f_xy.y1, y_sp),
)
F = sp.lambdify(sp.symbols('x, y'), F_sp)

print(f'F(x, y) = {sp.simplify(F_sp)}')

integral_value = sp.integrate(
    f_sp,
    (x_sp, f_xy.x1, f_xy.x2),
    (y_sp, f_xy.y1, f_xy.y2),
)
print(f'значение определенного интеграла функции плотности распределения равно 1:r{round(float(integral_value), 2)}')

##Одномерные плотности:

f_x_sp = sp.integrate(
    f_sp,
    (y_sp, f_xy.x1, f_xy.x2)
)
f_x = sp.lambdify(
    sp.symbols('x'),
    f_x_sp,
)

f_y_sp = sp.integrate(
    f_sp,
    (x_sp, f_xy.y1, f_xy.y2)
)
f_y = sp.lambdify(
    sp.symbols('y'),
    f_y_sp,
)

print(f'f(x) = {f_x_sp}')
print(f'f(y) = {f_y_sp}')

multiplied_sp = sp.Mul(f_x_sp, f_y_sp)
multiplied = sp.lambdify(sp.symbols('x, y'), multiplied_sp)
print(f'f(x)*f(y) = {sp.simplify(multiplied_sp)}')


# Условные плотности распределения
f_x_pipe_y_sp = f_sp / f_y_sp
f_y_pipe_x_sp = f_sp / f_x_sp

print(f'f(x | y) = {sp.simplify(f_x_pipe_y_sp)}')
print(f'f(y | x) = {sp.simplify(f_y_pipe_x_sp)}')

# 3D график плотности распределения

node_count = 16

x_list = np.linspace(
    f_xy.x1,
    f_xy.x2,
    node_count,
)
y_list = np.linspace(
    f_xy.y1,
    f_xy.y2,
    node_count,
)
X, Y = np.meshgrid(x_list, y_list)
Z = f(X, Y)

fig = plt.figure(label='Плотность распределения')
ax = plt.axes(projection='3d')
ax.plot_wireframe(X, Y, Z, color='green')

plt.show()


plt.clf()


# 3D график функции распределения

Z = F(X, Y)

fig = plt.figure(label='Функция распределения')
ax = plt.axes(projection='3d')
ax.plot_wireframe(
    X, Y, Z,
    color='green',
)

plt.show()


plt.clf()

# График разности f(x, y) и f(x)*f(y): если равны то св независимы

Z = f(X, Y) - multiplied(X, Y)
fig = plt.figure(label='Разность f(x, y) и f(x)*f(y)')
ax = plt.axes(projection='3d')
ax.plot_wireframe(X, Y, Z, color='green')

plt.show()
plt.clf()

 # Функция для генерации выборки по функции плотности:

sample_size = 1_000_000


def generate_sample(pdf_xy: FDenstity, sample_size=sample_size):
    sample = []
    while len(sample) < sample_size:
        x = pdf_xy.x1 + random.random() * (pdf_xy.x2 - pdf_xy.x1)
        y = pdf_xy.y1 + random.random() * (pdf_xy.y2 - pdf_xy.y1)
        z = random.random()
        if z < pdf_xy.pdf(x, y):
            sample.append((x, y))
    return sample

# Построение выборки:

sample = generate_sample(f_xy)
x_sample = [el[0] for el in sample]
y_sample = [el[1] for el in sample]

 # Ожидаемое и наблюдаемое математическое ожидание:

expected_mean = (
    scipy.integrate.dblquad(
        lambda x, y: x * f(x, y),
        f_xy.x1, f_xy.x2,
        f_xy.y1, f_xy.y2
    )[0],
    scipy.integrate.dblquad(
        lambda x, y: y * f(x, y),
        f_xy.x1, f_xy.x2,
        f_xy.y1, f_xy.y2
    )[0]
)
observed_mean = (
    np.mean(x_sample),
    np.mean(y_sample),
)

print(f'expected mean: {expected_mean}')
print(f'observed mean: {observed_mean}')

# Ожидаемая и наблюдаемая дисперсия:

expected_variance = (
    scipy.integrate.dblquad(
        lambda x, y: (x - expected_mean[0])**2 * f(x, y),
        f_xy.x1, f_xy.x2,
        f_xy.y1, f_xy.y2,
    )[0],
    scipy.integrate.dblquad(
        lambda x, y: (y - expected_mean[1])**2 * f(x, y),
        f_xy.x1, f_xy.x2,
        f_xy.y1, f_xy.y2,
    )[0],
)

observed_variance = (
    np.var(x_sample),
    np.var(y_sample),
)

print(f'expected variance: {expected_variance}')
print(f'observed variance: {observed_variance}')

# Ожидаемая и наблюдаемая корреляция:

expected_r_xy = scipy.integrate.dblquad(
    lambda x, y: ((x - expected_mean[0]) *
                  (y - expected_mean[1]) *
                  f(x, y)),
    f_xy.x1, f_xy.x2,
    f_xy.y1, f_xy.y2,
)[0] / np.sqrt(expected_variance[0] * expected_variance[1])

x_sample_centered = x_sample - observed_mean[0]
y_sample_centered = y_sample - observed_mean[1]

observed_r_xy = ((x_sample_centered @ y_sample_centered)
                 / (len(x_sample)
                    * np.sqrt(observed_variance[0]
                              * observed_variance[1])))

print(f'expected r_xy: {expected_r_xy}')
print(f'observed r_xy: {observed_r_xy}')

# Построение гистограммы для иксов:

fig = plt.figure(label='Гистограмма 1')

plt.hist(
    x_sample,
    density=True,
    bins=10,
)
plt.plot(
    x_list,
    f_x(x_list),
)
plt.show()
plt.clf()

# Построение гистограммы для игреков:

fig = plt.figure(label='Гистограмма 2')

plt.hist(
    y_sample,
    density=True,
    bins=10,
)
plt.plot(
    y_list,
    f_y(y_list),
)
plt.show()

plt.clf()

# Двумерная гистограмма + график плотности распределения:

fig = plt.figure(label='Гистограмма выборки + график плотности')
ax = fig.add_subplot(111, projection='3d')

bin_count = 16

x_bin_width = (f_xy.x2 - f_xy.x1) / bin_count
y_bin_width = (f_xy.y2 - f_xy.y1) / bin_count

hist, x_edges, y_edges = np.histogram2d(
    x_sample, y_sample,
    bins=bin_count,
    range=[
        [f_xy.x1, f_xy.x2],
        [f_xy.y1, f_xy.y2],
    ],
)

x_pos, y_pos = np.meshgrid(
    x_edges[:-1],
    y_edges[:-1],
)

# Нормализация высот столбцов гистограммы:
hist = hist / sample_size / x_bin_width / y_bin_width
heights = hist.flatten()

ax.bar3d(
    x_pos.flatten(), y_pos.flatten(), np.zeros(len(heights)),
    x_bin_width, y_bin_width, heights,
    alpha=0.1
)
Z = f(X, Y)
ax.plot_surface(
    X, Y, Z,
    color='green',
    antialiased=False
)

plt.show()

plt.clf()







