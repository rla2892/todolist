<template>
  <h1>Todo List App - Sangjin</h1>
  <div>
    <input id="addInput" v-model="addInputText" type="text" placeholder="Write here!!!">
    <button @click="onAdd" >Add</button>
  </div>
  <ul>
    <li v-for="todoItem of list" v-bind:key="todoItem" >
      <button @click="onSetDone(todoItem.id, !todoItem.isDone)" class="doneBtn">{{ todoItem.isDone ? 'V' : '&nbsp;' }}</button>
      <span>{{ todoItem }}</span>
      <button @click="onDel(todoItem.id)" >Delete</button>
    </li>
  </ul>
</template>

<script>
const API_URL = `http://localhost:53015`
// const API_URL = `https://localhost:44397`

export default {
  data () {
    return {
      list: null,
      addInputText: ''
    }
  },
  created () {
    // fetch on init
    this.fetchData()
  },
  methods: {
    async fetchData () {
      const url = `${API_URL}/api/todo/list`
      const res = await fetch(url, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
      const json = await res.json()
      this.list = json
    },
    async AddTodo (content) {
      const url = `${API_URL}/api/todo`
      await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          content: content
        })
      })
    },
    async SetDone (id, done) {
      const url = `${API_URL}/api/todo/${done ? 'done' : 'undo'}/${id}`
      await fetch(url, {
        method: 'PUT'
      })
    },
    async DeleteTodo (id) {
      const url = `${API_URL}/api/todo/${id}`
      await fetch(url, {
        method: 'DELETE'
      })
    },
    async onAdd (e) {
      await this.AddTodo(this.addInputText)
      this.addInputText = '' // add 함수 call 이후, input 비우기
      this.fetchData() // TODO : Add 이후 전체 fetch 가 아닌 add 된 것만 get 해서 this.list 에 추가해주기
    },
    async onDel (id) {
      await this.DeleteTodo(id)
      this.fetchData()
    },
    async onSetDone (id, done) {
      await this.SetDone(id, done)
      this.fetchData()
    }
  }
}
</script>

<style scoped>
ul {
  padding-inline-start: 0;
}
li {
  border-color: limegreen;
  border-width: 2px;
  border-style: solid;
  margin-bottom: 1rem;
  list-style: none;
}
.doneBtn {
  background-color: rgba(0,0,0,0);
  cursor: pointer;
}
.doneBtn:hover {
  background-color: rgba(0,0,0,0.1);
}
</style>
