import './style.css'
import dom from './dom'

class Clipic {
  constructor() {
    this.default = {
      width: 500, // 裁剪宽度
      height: 500, // 裁剪高度
      src: '', // 需要裁剪的图片
      type: 'jpeg', // 裁剪后图片的类型，仅支持jpeg/png两种
      quality: 0.9, // 压缩质量
      buttonText: ['取消', '重置', '完成'] // 底部三个按钮文案
    }
    this.init() // 初始化，渲染dom跟css
    this.clipic = this.getId('clipic')
    this.img1 = this.getId('clipicImg1') // 背景图
    this.img2 = this.getId('clipicImg2') // 前景图
    this.frame1 = this.getId('clipicFrame1') // 背景操作框
    this.frame2 = this.getId('clipicFrame2') // 前景操作框
    this.cancelBtn = this.getId('clipicCancel') // 取消按钮
    this.resetBtn = this.getId('clipicReset') // 重置按钮
    this.confirmBtn = this.getId('clipicConfirm') // 完成按钮
    this.reset = this.reset.bind(this)
    this.done = this.done.bind(this)
    this.cancel = this.cancel.bind(this)
  }

  init() {
    if (!this.getId('clipic')) {
      this.createHtml()
    }
  }

  getId(id) {
    return document.getElementById(id)
  }

  createHtml() {
    const div = document.createElement('div')
    div.className = 'clipic-body'
    div.setAttribute('id', 'clipic')
    div.innerHTML = dom
    document.body.appendChild(div)
  }

  getImage(options = {}) {
    // 初始化参数
    this.scale = 1 // 缩放
    this.rotate = 0 // 旋转
    this.translateX = 0 // 水平偏移
    this.translateY = 0 // 垂直偏移
    const defaults = JSON.parse(JSON.stringify(this.default))
    this.options = Object.assign(defaults, options)
    this.cancelBtn.innerHTML = this.options.buttonText[0]
    this.resetBtn.innerHTML = this.options.buttonText[1]
    this.confirmBtn.innerHTML = this.options.buttonText[2]
    this.img1.src = this.options.src
    this.img2.src = this.options.src
    let tempImage = new Image()
    tempImage.onload = () => {
      this.originW = this.img2.width
      this.originH = this.img2.height
      if (this.options.ratio) {
        this.options.width = this.img2.width
        this.options.height = this.img2.width / this.options.ratio
      } else {
        this.options.ratio = this.options.width / this.options.height
      }
      this.originRatio = this.originW / this.originH
      this.initSize()
      this.clipic.style.transform = 'translate(0, 0)'
      setTimeout(() => {
        if (this.options.ratio > this.originRatio) {
          this.img1.style.width = this.frame2.clientWidth + 'px'
          this.img2.style.width = this.frame2.clientWidth + 'px'
        } else {
          this.img1.style.height = this.frame2.clientHeight + 'px'
          this.img2.style.height = this.frame2.clientHeight + 'px'
        }
      }, 300)
      this.setTransform()
      this.cancelBtn.addEventListener('click', this.cancel)
      this.resetBtn.addEventListener('click', this.reset)
      this.confirmBtn.addEventListener('click', this.done)
      this.clipic.addEventListener('touchmove', e => {
        e.preventDefault()
        if (e.touches.length > 1) {
          this.setScale(e.touches[0], e.touches[1])
          this.setRotate(e.touches[0], e.touches[1])
          return
        }
        this.setTranslate(e.touches[0])
      })
      this.clipic.addEventListener('touchend', e => {
        this.distance = null
        this.angle = null
        this.moveX = null
        this.moveY = null
      })
    }
    tempImage.src = this.options.src
  }

  initSize() {
    const body = document.documentElement || document.body
    const cw = body.clientWidth - 60
    const ch = body.clientHeight - 80
    this.frame1.style.width = cw + 'px'
    this.frame1.style.height = cw / this.options.ratio + 'px'
    this.frame2.style.width = cw + 'px'
    this.frame2.style.height = cw / this.options.ratio + 'px'
    if (cw / this.options.ratio > ch) {
      this.frame1.style.height = ch + 'px'
      this.frame1.style.width = ch * this.options.ratio + 'px'
      this.frame2.style.height = ch + 'px'
      this.frame2.style.width = ch * this.options.ratio + 'px'
    }
  }

  setScale(touches1, touches2) {
    const x = Math.abs(touches1.clientX - touches2.clientX)
    const y = Math.abs(touches1.clientY - touches2.clientY)
    const s = Math.sqrt(x * x + y * y)
    if (this.distance) {
      this.scale += (s - this.distance) / this.img2.clientWidth
      this.setTransform()
    }
    this.distance = s
  }

  setRotate(touches1, touches2) {
    const x = touches1.clientX - touches2.clientX
    const y = touches1.clientY - touches2.clientY
    const s = Math.sqrt(x * x + y * y)
    const angle = (Math.atan2(y, x) * 180) / Math.PI
    if (this.angle) {
      this.rotate += angle - this.angle
      this.setTransform()
    }
    this.angle = angle
  }

  setTranslate(touches) {
    const x = touches.clientX
    const y = touches.clientY
    if (this.moveX) {
      this.translateX += x - this.moveX
    }
    if (this.moveY) {
      this.translateY += y - this.moveY
    }
    this.moveX = x
    this.moveY = y
    this.setTransform()
  }

  setTransform() {
    const transform = `translate(${this.translateX}px, ${this.translateY}px) scale(${this.scale}) rotate(${
      this.rotate
    }deg)`
    this.img1.style.transform = transform
    this.img2.style.transform = transform
  }

  cancel() {
    this.clipic.style.transform = 'translate(0, 100%)'
    setTimeout(() => {
      this.img1.style = ''
      this.img1.src = ''
      this.img2.style = ''
      this.img2.src = ''
    }, 400)
    if (this.options.onCancel) {
      this.options.onCancel()
    }
    this.cancelBtn.removeEventListener('click', this.cancel)
    this.resetBtn.removeEventListener('click', this.reset)
    this.confirmBtn.removeEventListener('click', this.done, true)
  }

  reset() {
    this.scale = 1
    this.rotate = 0
    this.translateX = 0
    this.translateY = 0
    this.img1.style.transition = '0.3s'
    this.img2.style.transition = '0.3s'
    this.setTransform()
    setTimeout(() => {
      this.img1.style.transition = ''
      this.img2.style.transition = ''
    }, 300)
  }

  done() {
    const zommRatio = this.options.width / this.frame2.clientWidth
    const canvas = document.createElement('canvas')
    canvas.width = this.options.width
    canvas.height = this.options.height
    const ctx = canvas.getContext('2d')
    ctx.fillStyle = '#fff'
    ctx.fillRect(0, 0, canvas.width, canvas.height)
    let drawImageW
    let drawImageH
    if (this.options.ratio > this.originRatio) {
      drawImageW = this.options.width
      drawImageH = this.originH / (this.originW / this.options.width)
    } else {
      drawImageH = this.options.height
      drawImageW = this.originW / (this.originH / this.options.height)
    }
    const point = { x: drawImageW / 2, y: drawImageH / 2 }
    ctx.translate(this.translateX * zommRatio, this.translateY * zommRatio)
    if (this.rotate !== 0) {
      ctx.translate(point.x, point.y)
      ctx.rotate((this.rotate * Math.PI) / 180)
      ctx.translate(-point.x, -point.y)
    }
    if (this.scale !== 1) {
      ctx.translate(point.x * (1 - this.scale), point.y * (1 - this.scale))
      ctx.scale(this.scale, this.scale)
    }
    ctx.drawImage(this.img2, 0, 0, drawImageW, drawImageH)
    if (this.options.onDone) {
      this.options.onDone(canvas.toDataURL(`image/${this.options.type}`, this.options.quality))
    }
    this.cancel()
  }
}
export default Clipic
