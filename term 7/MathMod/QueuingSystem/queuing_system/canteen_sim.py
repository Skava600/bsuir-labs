import simpy
import numpy as np
from functools import reduce

class Visitor:
    def __init__(self, q, number):
        self.number = number
        self.time_queue = 0
        self.time_checkout = 0
        self.time_ate = 0
        self.L_queue = 0
        self.L_checkout = 0
        self.L_canteen = 0
        self.count_dishes = 2 if np.random.random() < q else 1


# Singlechannel Canteen Simulation processes class
class CanteenSimulation:
    def __init__(self, X, mu, q, t, env):
        # queuing system params
        self.X = X
        self.mu = mu
        self.q = q
        self.t = t
        # empirical values lists:
        # 1) queue - visitors in the queue
        # 2) checkout - visitors in the queue and cash desk
        # 3) canteen - visitors in the queue, cash desk and overall canteen (while eating)
        self.L_canteen_list = []
        self.L_checkout_list = []
        self.L_queue_list = []
        self.t_canteen_list = []
        self.t_checkout_list = []
        self.t_queue_list = []
        self.visitors = []
        # enviromental variables for simpy simulation
        self.env = env
        self.checkout_performer = simpy.Resource(env)
        self.eating_performer = simpy.Resource(env, 5000)

    # Singlechannel Canteen Simulation with parameters X, mu, v, test_time
    @staticmethod
    def simulate_canteen(X, mu, q, t, test_time):
        env = simpy.Environment()
        canteen = CanteenSimulation(X, mu, q, t, env)
        env.process(canteen.process_visitors_queue())
        env.process(canteen.process_visitors())
        env.run(until=test_time)
        return canteen

    def process_visitors(self):
        while True:
            yield self.env.timeout(0.01)
            while len(self.visitors) != 0:
                visitor = self.visitors.pop(0)
                self.env.process(self.make_eat_request(visitor))

    def process_visitors_queue(self):
        visitors_counter = 1
        while True:
            yield self.env.timeout(np.random.exponential(1/self.X))
            visitor = Visitor(self.q, visitors_counter)
            visitors_counter = visitors_counter + 1
            self.env.process(self.make_queue_request(visitor))

    def make_eat_request(self, visitor):
        eating_count_before = self.eating_performer.count

        with self.eating_performer.request() as request:
            visitor.L_canteen = visitor.L_checkout + eating_count_before
            arrival_time = self.env.now

            yield request
            for _ in range(visitor.count_dishes):
                yield self.env.process(self.request_eating())
            visitor.time_ate = visitor.time_checkout + self.env.now - arrival_time
            self.L_canteen_list.append(visitor.L_canteen)
            self.L_checkout_list.append(visitor.L_checkout)
            self.L_queue_list.append(visitor.L_queue)
            self.t_canteen_list.append(visitor.time_ate)
            self.t_checkout_list.append(visitor.time_checkout)
            self.t_queue_list.append(visitor.time_queue)

    def make_queue_request(self, visitor):
        queque_len_before = len(self.checkout_performer.queue)
        queque_count_before = self.checkout_performer.count

        with self.checkout_performer.request() as request:
            visitor.L_queue = queque_len_before
            visitor.L_checkout = queque_len_before + queque_count_before
            arrival_time = self.env.now

            yield request
            visitor.time_queue = self.env.now - arrival_time

            for _ in range(visitor.count_dishes):
                yield self.env.process(self.request_checkout_processing())
            visitor.time_checkout = self.env.now - arrival_time
            self.visitors.append(visitor)

    def request_eating(self):
        yield self.env.timeout(np.random.exponential(self.t))

    def request_checkout_processing(self):
        yield self.env.timeout(np.random.exponential(1/self.mu))

    def get_results(self):
        return self.L_canteen_list, self.L_queue_list, self.t_canteen_list, self.t_queue_list


def calculate_empirical_characteristics(canteen):
    L_canteen_list, L_queue_list, t_canteen_list, t_queue_list = canteen.get_results()
    p = []
    i = 0
    for i in range(30):
        request_frequency = reduce(lambda count, x: count+1 if x == i else count, L_canteen_list, 0)
        p_i = request_frequency / len(L_canteen_list)
        p.append(p_i)


    L_canteen = sum(L_canteen_list) / len(L_canteen_list)
    L_queue = sum(L_queue_list) / len(L_queue_list)
    t_canteen = sum(t_canteen_list) / len(t_canteen_list)
    t_queue = sum(t_queue_list) / len(t_queue_list)

    return p, L_canteen, L_queue, t_canteen, t_queue