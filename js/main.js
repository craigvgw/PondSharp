(()=>{"use strict";var t={5305:(t,i,e)=>{var s=e(8244);const n=window;class o{constructor(t,i,e,s,n){this.entities={},this.isMouseDown=!1,this.start=()=>this.application.start(),this.stop=()=>this.application.stop(),this.pondRef=t,this.element=i,this.width=e,this.height=s,this.gridSize=n,this.init()}init(){this.application&&(console.debug("Removing PIXI instance"),this.application.destroy(!0,{children:!0,texture:!0,baseTexture:!0}),this.element.removeChild(this.element.children.item(0)),this.application=null),console.debug("Creating PIXI instance"),this.application=new s.Mx({width:this.width*this.gridSize,height:this.height*this.gridSize}),this.application.renderer.plugins.interaction.cursorStyles.default="none",this.element.appendChild(this.application.view),this.application.stage.x=this.application.view.width/2,this.application.stage.y=this.application.view.height/2,this.application.stage.width=this.application.view.width,this.application.stage.height=this.application.view.height,this.application.ticker.add((()=>this.onTick())),this.entityHolder=new s.W2,this.application.stage.addChild(this.entityHolder),this.fps=new s.xv("FPS: ??",{fill:11206570,fontSize:8}),this.fps.x=-this.application.view.width/2,this.fps.y=-this.application.view.height/2,this.application.stage.addChild(this.fps),this.cursor=new s.TC,this.cursor.beginFill(16777215),this.cursor.drawCircle(this.gridSize/2,this.gridSize/2,this.gridSize/2),this.cursor.endFill(),this.cursor.position.x=0,this.cursor.position.y=0,this.application.stage.addChild(this.cursor),this.application.stage.interactive=!0,this.application.view.addEventListener("mousemove",(t=>this.onMouseMove(t))),this.application.view.addEventListener("mousedown",(t=>this.onMouseDown(t))),this.application.view.addEventListener("mouseup",(t=>this.onMouseUp(t)))}onMouseMove(t){const i=this.getGridPosition(t);this.cursor.position.x=i.x*this.gridSize,this.cursor.position.y=i.y*this.gridSize,!this.isMouseDown||this.lastMouseLocation.x===i.x&&this.lastMouseLocation.y===i.y||this.onClick(i.x,i.y),this.lastMouseLocation=i}onMouseDown(t){t.stopPropagation(),this.isMouseDown=!0;const i=this.getGridPosition(t);this.lastMouseLocation=i,this.onClick(i.x,i.y)}onMouseUp(t){t.stopPropagation(),this.isMouseDown=!1}onClick(t,i){this.pondRef.invokeMethodAsync("OnClick",t,i)}getGridPosition(t){const i=t.offsetX-this.application.stage.x,e=t.offsetY-this.application.stage.y;return{x:Math.round(i/this.gridSize),y:Math.round(e/this.gridSize)}}onTick(){this.fps.text=Math.round(s.vB.shared.FPS)+" fps"}resize(t,i,e){this.width=t,this.height=i,this.gridSize=e,this.gridSize=e,this.init()}createEntity(t,i,e,n){const o=new s.TC;o.beginFill(n),o.drawCircle(this.gridSize/2,this.gridSize/2,this.gridSize/2),o.endFill(),o.position.x=i*this.gridSize,o.position.y=e*this.gridSize,this.entities[t]=o,this.entityHolder.addChild(o)}destroyEntity(t){this.entities[t]&&(this.entities[t].destroy(),this.entities[t]=null,delete this.entities[t])}moveEntity(t,i,e){if(!this.entities[t])return;const s=this.entities[t];s.position.x=i*this.gridSize,s.position.y=e*this.gridSize}changeEntityColor(t,i){if(!this.entities[t])return;const e=this.entities[t];e.clear(),e.beginFill(i),e.drawCircle(this.gridSize/2,this.gridSize/2,this.gridSize/2),e.endFill()}processEntityChangeRequestsRaw(t){const i=n.Blazor.platform.getArrayLength(t);for(let e=0;e<i;e++){const i=n.Blazor.platform.getArrayEntryPtr(t,e,16),s=n.Blazor.platform.readInt32Field(i,0);if(this.entities[s])switch(n.Blazor.platform.readInt32Field(i,4)){case 1:const t=n.Blazor.platform.readInt32Field(i,8),e=n.Blazor.platform.readInt32Field(i,12);this.moveEntity(s,t,e);break;case 2:const o=n.Blazor.platform.readInt32Field(i,8);this.changeEntityColor(s,o);break;case 0:return!0;default:throw new Error("unknown type")}}return!0}}var r=e(7955);class a{constructor(t){this.elementId=t,this.editor=r.j6.create(document.getElementById(t),{value:"",language:"csharp",theme:"vs-dark",automaticLayout:!0})}setCode(t){this.editor.setValue(t)}getCode(){return this.editor.getValue()}}var h=function(t,i,e,s){return new(e||(e=Promise))((function(n,o){function r(t){try{h(s.next(t))}catch(t){o(t)}}function a(t){try{h(s.throw(t))}catch(t){o(t)}}function h(t){var i;t.done?n(t.value):(i=t.value,i instanceof e?i:new e((function(t){t(i)}))).then(r,a)}h((s=s.apply(t,i||[])).next())}))};const l=window;l.ponds={},l.lastPond=0,l.createPond=function(t,i,e,s,n){return h(this,void 0,void 0,(function*(){l.lastPond++;const r="pond_"+l.lastPond;return l.ponds[r]=new o(t,i,e,s,n),r}))},l.createEditor=function(t,i){return h(this,void 0,void 0,(function*(){l[i]=new a(t)}))},l.getSize=()=>[l.innerWidth,l.innerHeight]}},i={};function e(s){if(i[s])return i[s].exports;var n=i[s]={id:s,loaded:!1,exports:{}};return t[s].call(n.exports,n,n.exports,e),n.loaded=!0,n.exports}e.m=t,e.n=t=>{var i=t&&t.__esModule?()=>t.default:()=>t;return e.d(i,{a:i}),i},e.d=(t,i)=>{for(var s in i)e.o(i,s)&&!e.o(t,s)&&Object.defineProperty(t,s,{enumerable:!0,get:i[s]})},e.e=()=>Promise.resolve(),e.g=function(){if("object"==typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(t){if("object"==typeof window)return window}}(),e.o=(t,i)=>Object.prototype.hasOwnProperty.call(t,i),e.r=t=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},e.nmd=t=>(t.paths=[],t.children||(t.children=[]),t),e.p="js/",(()=>{var t={179:0},i=[[5305,216]],s=()=>{};function n(){for(var s,n=0;n<i.length;n++){for(var o=i[n],r=!0,a=1;a<o.length;a++){var h=o[a];0!==t[h]&&(r=!1)}r&&(i.splice(n--,1),s=e(e.s=o[0]))}return 0===i.length&&(e.x(),e.x=()=>{}),s}e.x=()=>{e.x=()=>{},r=r.slice();for(var t=0;t<r.length;t++)o(r[t]);return(s=n)()};var o=n=>{for(var o,r,[h,l,d,c]=n,p=0,u=[];p<h.length;p++)r=h[p],e.o(t,r)&&t[r]&&u.push(t[r][0]),t[r]=0;for(o in l)e.o(l,o)&&(e.m[o]=l[o]);for(d&&d(e),a(n);u.length;)u.shift()();return c&&i.push.apply(i,c),s()},r=self.webpackChunkpondsharp_client=self.webpackChunkpondsharp_client||[],a=r.push.bind(r);r.push=o})(),e.x()})();