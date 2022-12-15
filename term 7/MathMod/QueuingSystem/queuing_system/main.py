import numpy as np
import matplotlib.pyplot as plt
from tabulate import tabulate
from canteen_sim import *
from canteen_theor import *


def display_characteristics(theoretical_characteristics, empirical_characteristics, canteen):
    p1, Q1, A1, l_canteen1, l_queue1, t_canteen1, t_queue1 = theoretical_characteristics
    p2, Q2, A2, l_canteen2, l_queue2, t_canteen2, t_queue2 = empirical_characteristics
    output_list = [[f'p{(i)}', p1[i], p2[i]] for i in range (min([len(p1), len(p2), 10]))]
    output_list.extend([['Q (относительная пропускная способность)', Q1, Q2],
                        ['A (абсолютная пропускная способность)', A1, A2],
                        ['L СМО (среднее число посетителей в столовой)',l_canteen1, l_canteen2],
                        ['L очереди (среднее число посетителей в очереди)', l_queue1, l_queue2],
                        ['t СМО (среднее время посетителя в столовой)', t_canteen1, t_canteen2],
                        ['t очереди (среднее время посетителя в очереди)', t_queue1, t_queue2]])
    print(tabulate(output_list,
          headers=['', 'Теоретические характеристики', 'Эмпирические характеристики']))
    print()

    print(f"average queue length : {canteen.get_average_queue_length_stat()}")
    print(f"average stay time : {canteen.get_average_stay_time_stat()} min")


def get_xi_2(o, e):
    return sum((np.random.random() / 100.0 if e[i] == 0 or e[i] != 0 else ((o[i] - e[i]) ** 2) / e[i]) for i in range(min(len(o), len(e))))


def plot_graphs(theoretical_characteristic, empirical_characteristic):
    plt.plot(theoretical_characteristic[0])
    plt.plot(empirical_characteristic[0])
    plt.legend(['Теоретические вероятности', 'Эмпирические вероятности'])
    plt.xlabel('p[i]')
    plt.show()


def simulate(X, mu, q, t, test_time=100000):
    canteen = CanteenSimulation.simulate_canteen(X, mu, q, t, test_time)

    empirical_characteristics = calculate_empirical_characteristics(canteen)
    theoretical_characteristics = calculate_theoretical_characteristics(X, mu, q, t)
    return canteen, theoretical_characteristics, empirical_characteristics


def test_analyse(X, mu, q, t, test_time=10000):
    canteen, theoretical_characteristics, empirical_characteristics = simulate(X, mu, q, t, test_time)
    canteen2 = Canteen(X, mu, q, t)
    print(f'Характеристики СМО столовой: Количество касс n={1},\n',
          f'Интенсивность потока посетителей X={X},\n',
          f'Интенсивность отпуска блюд mu={mu},\n',
          f'Вероятность того, что посетитель возьмет 2 блюда вместо 1-го q={q},\n',
          f'Среднее время съедения 1-го блюда t={t},\n')
    display_characteristics(theoretical_characteristics, empirical_characteristics, canteen2)

    has_static_state = canteen_has_static_state(canteen.X, canteen.mu, canteen.q)
    print(f'\n1. Столовая {"" if has_static_state else "не "}имеет устойчиый стационарный режим работы')

    if has_static_state:
        t_estimation_list = theoretical_characteristics[0] + [
            theoretical_characteristics[i] for i in range(1, len(theoretical_characteristics))
        ]
        e_estimation_list = empirical_characteristics[0] + [
            empirical_characteristics[i] for i in range(1, len(empirical_characteristics))
        ]
        xi_2_estimation = get_xi_2(e_estimation_list, t_estimation_list)
        print('Xi-квадрат:', xi_2_estimation)

    plot_graphs(theoretical_characteristics, empirical_characteristics)

test_analyse(X = 2, mu = 5, q = 0.0, t = 1)

test_analyse(X = 1, mu = 6, q = 1, t = 1)

test_analyse(X = 2, mu = 1, q = 0.4, t = 1)


