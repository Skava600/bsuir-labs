from typing import NamedTuple


class FDenstity(NamedTuple):
    pdf: callable
    x1: float
    x2: float
    y1: float
    y2: float
