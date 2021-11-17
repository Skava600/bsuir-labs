import os, sys, time

print(f'parent pid: {os.getpid()}')
for i in range(10):
    p_id = os.fork()
    if p_id == 0:
        sys.exit()
time.sleep(10)
os.wait()